using Raven.Client.Documents;

namespace BoostRoom.Infrastructure
{
    public static class BoostRoomDocumentStore
    {
        public static IDocumentStore Create(string host, string database)
        {
            var store = new DocumentStore
            {
                Urls = new[] 
                {
                   host 
                },
                Database = database, 
                Conventions = { }
            }.Initialize();

            return store;
        }
    }
}