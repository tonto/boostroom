using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Embedded;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    public abstract class EmbeddedRavenDbTest : IDisposable
    {
        protected readonly IDocumentStore DocumentStore; 
        
        protected EmbeddedRavenDbTest()
        {
            EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                CommandLineArgs = new List<string>() {"RunInMemory=true"}
            });

            DocumentStore = EmbeddedServer.Instance.GetDocumentStore("EmbeddedDB");
        }

        public void Dispose()
        {
            DocumentStore.Dispose();
            EmbeddedServer.Instance.Dispose();
        }
    }
}