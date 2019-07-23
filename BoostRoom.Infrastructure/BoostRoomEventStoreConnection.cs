using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace BoostRoom.Infrastructure
{
    public static class BoostRoomEventStoreConnection
    {
        private static IEventStoreConnection _connection;
        
        public static async Task<IEventStoreConnection> ConnectAsync()
        {
            if (_connection != null) return _connection;
            
            var endpoint = new Uri("tcp://127.0.0.1:1113");

            var settings = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));


            _connection = EventStoreConnection.Create(settings, endpoint);

            await _connection.ConnectAsync();

            return _connection;
        }
    }
}