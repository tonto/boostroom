using Raven.Client.Documents;

namespace BoostRoom.Infrastructure
{
    public static class BoostRoomDocumentStore
    {
        public static IDocumentStore Create()
        {
            var store = new DocumentStore
            {
                Urls = new[] // URL to the Server,
                {
                    // or list of URLs 
                    "http://localhost:8080" // to all Cluster Servers (Nodes)
                },
                Database = "Northwind", // Default database that DocumentStore will interact with
                Conventions = { }
            }.Initialize();

            return store;
        }
    }
}