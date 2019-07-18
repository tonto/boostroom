using System;
using EventStore.ClientAPI;
using Raven.Client.Documents;
using Raven.Embedded;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    [Collection("IntegrationCollection")]
    public abstract class IntegrationAbstractTest : IDisposable  
    {
        protected readonly IDocumentStore DocumentStore;
        protected readonly IEventStoreConnection EventStoreConnection;
        
        protected IntegrationAbstractTest(IntegrationFixture fixture)
        {
            DocumentStore = EmbeddedServer.Instance.GetDocumentStore(Guid.NewGuid().ToString());
            EventStoreConnection = fixture.EventStoreConnection;
        }

        public void Dispose()
        {
            DocumentStore?.Dispose();
        }
    }
}