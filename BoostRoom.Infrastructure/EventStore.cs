using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Tactical.DDD;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Infrastructure
{
    public class EventStore : IEventStore
    {
        // TODO Implement IDisposable and dispose of _connection

        private readonly IEventStoreConnection _connection;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public EventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadEventsAsync(IEntityId aggregateId)
        {
            var stream = $"{aggregateId.GetType().Name}-{aggregateId.ToString()}";

            var streamEvents = await _connection.ReadStreamEventsForwardAsync(stream, 0, 4096, false);

            return streamEvents.Events.Select(e =>
            {
                var @event = JsonConvert.DeserializeObject(Encoding.ASCII.GetString(e.Event.Data), _serializerSettings);
                return @event as IDomainEvent;
            }).ToList().AsReadOnly();
        }

        public async Task SaveEventsAsync(IEntityId aggregateId, int version, IReadOnlyCollection<IDomainEvent> events)
        {
            var stream = $"{aggregateId.GetType().Name}-{aggregateId.ToString()}";

            await _connection.AppendToStreamAsync(stream, version, events.Select(e =>
            {
                var json = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(e, _serializerSettings));
                return new EventData(Guid.NewGuid(), e.GetType().Name, true, json, new byte[] { });
            }));
        }
    }
}