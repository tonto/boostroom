using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Tactical.DDD;

namespace BoostRoom.Integration.Tests
{
    public static class EventStoreExtensions
    {
        public static async Task<IReadOnlyCollection<IDomainEvent>> LoadAllEventsAsync(
            this BoostRoom.Infrastructure.EventStore eventStore)
        {
            var streamEvents = await eventStore.Connection.ReadAllEventsForwardAsync(Position.Start, 4096, false);

            return streamEvents.Events
                .Where(e => e.Event.EventStreamId[0] != '$')
                .Select(BoostRoom.Infrastructure.EventStore.DeserializeDomainEvent)
                .ToList()
                .AsReadOnly();
        }
        
        public static async Task SaveEventsAsync(this BoostRoom.Infrastructure.EventStore eventStore, string stream, int version, IReadOnlyCollection<IDomainEvent> events)
        {
            await eventStore.Connection.AppendToStreamAsync(stream, version, events.Select(e =>
            {
                var json = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(e, BoostRoom.Infrastructure.EventStore.SerializerSettings));
                return new EventData(Guid.NewGuid(), e.GetType().Name, true, json, new byte[] { });
            }));
        }
    }
}