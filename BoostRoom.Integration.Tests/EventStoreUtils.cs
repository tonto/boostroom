using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Tactical.DDD;

namespace BoostRoom.Integration.Tests
{
    public static class EventStoreUtils
    {
        private static IEventStoreConnection _connection;

        public static IReadOnlyCollection<IDomainEvent> LoadEventsAsync(string stream)
        {
            EnsureConnection();

            var streamEvents =
                _connection.ReadStreamEventsForwardAsync(stream, 0, 100, false).Result;

            //return streamEvents.Events.Select(e =>
            //{
            //    // TODO Deserialize
            //});

            return null;
        }

        private static void EnsureConnection()
        {
            if (_connection == null)
            {
                _connection =
                    EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

                _connection.ConnectAsync().Wait();
            }
        }
    }
}