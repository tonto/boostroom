using System;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Accounts.Domain.SellerAggregate;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain
{
    public sealed class RegistrationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly IUniqueAccountsRepository _uniqueAccountsRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IEventStore _eventStore;

        public RegistrationService(
            IPasswordEncoder passwordEncoder,
            IUniqueAccountsRepository uniqueAccountsRepository,
            IClientRepository clientRepository,
            IEventStore eventStore,
            IEmailSender emailSender,
            ISellerRepository sellerRepository)
        {
            _passwordEncoder = passwordEncoder;
            _uniqueAccountsRepository = uniqueAccountsRepository;
            _clientRepository = clientRepository;
            _eventStore = eventStore;
            _emailSender = emailSender;
            _sellerRepository = sellerRepository;
        }

        public async Task RegisterClient(
            string username,
            string email,
            string password,
            string firstName,
            string lastName,
            string address,
            string city,
            string zip,
            string country,
            bool subscribeToOffers
        )
        {
            var isUnique = await _uniqueAccountsRepository.AreUnique(username, email);

            if (!isUnique)
            {
                throw new UsernameEmailTakenException("Username or Email is already registered.");
            }

            var client = Client.FromDetails(
                _clientRepository.NextId(),
                username,
                email,
                _passwordEncoder.Encode(password),
                firstName,
                lastName,
                address,
                city,
                zip,
                country,
                subscribeToOffers);

            await _eventStore.SaveEventsAsync(client.Id, -1, client.DomainEvents);

            await SendClientConfirmationEmail(firstName, email);
        }

        private async Task SendClientConfirmationEmail(string name, string email)
        {
            var code = Guid.NewGuid();
            var mail = $@"
Congratulations {name}!<br /><br />
Your BoostRoom account has been created.<br />
Click <a href='http://18.188.124.36/accounts/confirm-email/{code}'>here</a> to confirm your email.<br /><br />
BoostRoom";

            await _emailSender.SendEmailAsync(email, "BoostRoom Email Confirmation", mail);
        }

        public async Task RegisterSeller(
            string username,
            string email,
            string password,
            string country
        )
        {
            var seller = Seller.FromDetails(
                _sellerRepository.NextId(),
                username,
                email,
                country,
                _passwordEncoder.Encode(password));

            await _eventStore.SaveEventsAsync(seller.Id, -1, seller.DomainEvents);

            await SendSellerConfirmationEmail(username, email);
        }

        private async Task SendSellerConfirmationEmail(string name, string email)
        {
            var code = Guid.NewGuid();
            var mail = $@"
Congratulations {name}!<br /><br />
Your BoostRoom account has been created.<br />
We will contact you once your account has been reviewed.<br /><br />
BoostRoom";

            await _emailSender.SendEmailAsync(email, "BoostRoom Email Confirmation", mail);
        }
    }
}
