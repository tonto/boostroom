using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Embedded;
using Xunit;

namespace BoostRoom.Integration.Tests
{
    public class EmbeddedRavenDbFixture : IDisposable 
    {
        public EmbeddedRavenDbFixture()
        {
               EmbeddedServer.Instance.StartServer(new ServerOptions
            {
                CommandLineArgs = new List<string>() {"RunInMemory=true"}
            });
        }
        
        public void Dispose()
        {
            EmbeddedServer.Instance.Dispose();
        }
    }
}