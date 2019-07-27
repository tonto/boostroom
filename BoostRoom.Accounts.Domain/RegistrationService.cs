using System;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain.ClientAggregate;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain
{
    public sealed class RegistrationService
    {
        private readonly IPasswordEncoder _passwordEncoder;
        private readonly IUniqueAccountsRepository _uniqueAccountsRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IEventStore _eventStore;

        public RegistrationService(
            IPasswordEncoder passwordEncoder,
            IUniqueAccountsRepository uniqueAccountsRepository,
            IClientsRepository clientsRepository,
            IEventStore eventStore)
        {
            _passwordEncoder = passwordEncoder;
            _uniqueAccountsRepository = uniqueAccountsRepository;
            _clientsRepository = clientsRepository;
            _eventStore = eventStore;
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
            if (!await _uniqueAccountsRepository.AreUnique(username, email))
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
        }
    }
}