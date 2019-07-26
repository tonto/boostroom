using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace BoostRoom.Infrastructure
{
    public static class BoostRoomEventStoreConnection
    {
        private static IEventStoreConnection _connection;
        
        public static async Task<IEventStoreConnection> ConnectAsync(string host, string username, string password)
        {
            if (_connection != null) return _connection;
            
            var endpoint = new Uri(host);

            var settings = ConnectionSettings
                .Create()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials(username, password));

            _connection = EventStoreConnection.Create(settings, endpoint);

            await _connection.ConnectAsync();

            return _connection;
        }
    }
}