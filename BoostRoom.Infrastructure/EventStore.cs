using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Newtonsoft.Json;
using Tactical.DDD;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Infrastructure
{
    public class EventStore : IEventStore
    {
        // TODO Implement IDisposable and dispose of _connection

        public readonly IEventStoreConnection Connection;

        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public EventStore(IEventStoreConnection connection)
        {
            Connection = connection;
        }

        public static IDomainEvent DeserializeDomainEvent(ResolvedEvent e)
        {
            var @event =
                JsonConvert.DeserializeObject(Encoding.ASCII.GetString(e.Event.Data), SerializerSettings);

            var domainEvent = @event as IDomainEvent;

            if (domainEvent != null)
                domainEvent.CreatedAt = e.Event.Created;

            return domainEvent;
        }

        public async Task<IReadOnlyCollection<IDomainEvent>> LoadEventsAsync(IEntityId aggregateId)
        {
            var stream = $"{aggregateId.GetType().Name}-{aggregateId.ToString()}";

            var streamEvents = await Connection.ReadStreamEventsForwardAsync(stream, 0, 4096, false);

            return streamEvents.Events
                .Select(DeserializeDomainEvent)
                .ToList()
                .AsReadOnly();
        }

        public async Task SaveEventsAsync(IEntityId aggregateId, int version, IReadOnlyCollection<IDomainEvent> events)
        {
            var stream = $"{aggregateId.GetType().Name}-{aggregateId.ToString()}";

            await Connection.AppendToStreamAsync(stream, version, events.Select(e =>
            {
                var json = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(e, SerializerSettings));
                return new EventData(Guid.NewGuid(), e.GetType().Name, true, json, new byte[] { });
            }));
        }

        public void SubscribeToAll(string subscriptionName, Action<IDomainEvent> action)
        {
            Connection.SubscribeToAllFrom(Position.Start, CatchUpSubscriptionSettings.Default, (_, e) =>
            {
                if (e.Event.EventStreamId[0] == '$') return;

                var domainEvent = DeserializeDomainEvent(e);

                action(domainEvent);
            });
        }
    }
}