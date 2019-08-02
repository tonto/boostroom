using System.Collections.Generic;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using BoostRoom.Accounts.Domain.SellerAggregate;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.OngoingTasks;

namespace BoostRoom.Infrastructure.Accounts.RavenDB
{
    public class UniqueAccountsProjection 
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

        public void On(SellerRegistered @event)
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

        public class AccountEntry
        {
            public string Username { get; set; }

            public string Email { get; set; }
        }
    }
}