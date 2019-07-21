using System.Collections.Generic;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.OngoingTasks;

namespace BoostRoom.Infrastructure.Accounts.RavenDB
{
    // TODO - Dont implement IUniqueAccountsProjection - it should only have AreUnique (act as repo)
    // Move this class to a separate ProjectionHost
    public class UniqueAccountsProjection : IUniqueAccountsProjection
    {
        private readonly IDocumentStore _documentStore;

        public UniqueAccountsProjection(IDocumentStore documentStore, IEventStore eventStore)
        {
            _documentStore = documentStore;

            eventStore.SubscribeToAll(
                GetType().Name,
                e => ((dynamic) this).On((dynamic) e));
            
            // TODO - What about acks and redelivery ?
        }

        public void On(ClientRegistered @event)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(new AccountEntry
                {
                    Email = @event.Email,
                    Username = @event.Username
                });

                session.SaveChanges();
            }
        }

        public Task<bool> AreUnique(string username, string email)
        {
            return Task.FromResult(true);
        }

        public class AccountEntry
        {
            public string Username { get; set; }

            public string Email { get; set; }
        }
    }
}