using System;
using System.Threading.Tasks;
using BoostRoom.Infrastructure;
using EventStore.ClientAPI;
using Tactical.DDD;
using Tactical.DDD.EventSourcing;
using Tactical.DDD.Testing;
using Xunit;
using IEventStore = Tactical.DDD.EventSourcing.IEventStore;

namespace BoostRoom.Integration.Tests.Infrastructure
{
    public class EventStoreTests : IntegrationAbstractTest 
    {
        private readonly IEventStore _eventStore;

        public EventStoreTests(IntegrationFixture fixture) : base(fixture)
        {
            _eventStore = new BoostRoom.Infrastructure.EventStore(EventStoreConnection);
        }

        [Fact]
        public async Task LoadEventsAsync_Loads_Saved_Events()
        {
            var id = new TestId();
            var createdAt = DateTime.Now;
            var name = "John";

            var events = new IDomainEvent[]
            {
                new TestEvent(id.ToString(), createdAt, name)
            };

            await _eventStore.SaveEventsAsync(id, -1, events);

            var loadedEvents = await _eventStore.LoadEventsAsync(id);

            loadedEvents.ExpectOne<TestEvent>(e =>
            {
                Assert.Equal(id.ToString(), e.AggregateId);
                Assert.True(createdAt > DateTime.MinValue);
                Assert.Equal(name, e.Name);
            });
        }
    }
}