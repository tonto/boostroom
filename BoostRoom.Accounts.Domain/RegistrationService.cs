using System;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain
{
    public sealed class RegistrationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly IUniqueAccountsRepository _uniqueAccountsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IEventStore _eventStore;

        public RegistrationService(
            IPasswordEncoder passwordEncoder,
            IUniqueAccountsRepository uniqueAccountsRepository,
            IClientsRepository clientsRepository,
            IEventStore eventStore, IEmailSender emailSender)
        {
            _passwordEncoder = passwordEncoder;
            _uniqueAccountsRepository = uniqueAccountsRepository;
            _clientsRepository = clientsRepository;
            _eventStore = eventStore;
            _emailSender = emailSender;
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
                _clientsRepository.NextId(),
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

            await SendConfirmationEmail(firstName, email);
        }

        private async Task SendConfirmationEmail(string name, string email)
        {
            var code = Guid.NewGuid();
            var mail = $@"
Congratulations {name}!<br /><br />
Your BoostRoom account has been created.<br />
Click <a href='http://18.188.124.36/accounts/confirm-email/{code}'>here</a> to confirm your email.<br /><br />
BoostRoom";

            await _emailSender.SendEmailAsync(email, "BoostRoom Email Confirmation", mail);
        }
    }
}