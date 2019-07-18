using System;
using Raven.Client.Documents;
using Raven.Embedded;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    [Collection("RavenDBCollection")]
    public abstract class EmbeddedRavenDbAbstractTest : IDisposable  
    {
        protected readonly IDocumentStore DocumentStore;
        
        protected EmbeddedRavenDbAbstractTest(EmbeddedRavenDbFixture ravenDbFixture)
        {
            DocumentStore = EmbeddedServer.Instance.GetDocumentStore(Guid.NewGuid().ToString());
        }

        public void Dispose()
        {
            DocumentStore?.Dispose();
        }
    }
}