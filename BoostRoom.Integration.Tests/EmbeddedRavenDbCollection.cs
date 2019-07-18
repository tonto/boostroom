using Xunit;

namespace BoostRoom.Integration.Tests
{
    [CollectionDefinition("RavenDBCollection")]
    public class EmbeddedRavenDbCollection : ICollectionFixture<EmbeddedRavenDbFixture>
    {
        
    }
}