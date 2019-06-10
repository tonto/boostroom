using System.Threading;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class Client
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

            client.On(new ClientRegistered(
                username,
                email,
                encryptedPassword,
                firstName,
                lastName,
                addressLine,
                city,
                zip,
                country,
                subscribeToOffers));

            return client;
        }

        private void On(ClientRegistered @event)
        {
            Username = new Username(@event.Username);
            Email = new Email(@event.Email);
            FullName = new FullName(@event.FirstName, @event.LastName);
            Address = new Address(@event.AddressLine, @event.City, @event.Zip, @event.Country);
            Password = new Password(@event.EncryptedPassword);
        }
    }
}