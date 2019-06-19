using System.Threading;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class Client : AggregateRoot<ClientId>
    {
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public FullName FullName { get; private set; }
        public Address Address { get; private set; }
        public Password Password { get; private set; }

        private Client()
        {
        }

        public static Client RegisterWithDetails(
            ClientId id,
            string username,
            string email,
            string encryptedPassword,
            string firstName,
            string lastName,
            string addressLine,
            string city,
            string zip,
            string country,
            bool subscribeToOffers)
        {
            var client = new Client();

            client.Apply(
                new ClientRegistered(
                    id,
                    username,
                    email,
                    encryptedPassword,
                    firstName,
                    lastName,
                    addressLine,
                    city,
                    zip,
                    country,
                    subscribeToOffers)
            );

            return client;
        }

        public void On(ClientRegistered @event)
        {
            Id = @event.ClientId;
            Username = new Username(@event.Username);
            Email = new Email(@event.Email);
            FullName = new FullName(@event.FirstName, @event.LastName);
            Address = new Address(@event.AddressLine, @event.City, @event.Zip, @event.Country);
            Password = new Password(@event.EncryptedPassword);
        }
    }
}