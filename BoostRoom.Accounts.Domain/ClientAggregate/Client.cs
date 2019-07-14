using System.Threading;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class Client : AggregateRoot<ClientId>
    {
        private Client()
        {
        }

        public static Client FromDetails(
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
                    id.ToString(),
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
            Id = new ClientId(@event.AggregateId);
        }
    }
}