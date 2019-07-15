using System;
using Newtonsoft.Json;
using Tactical.DDD;

namespace BoostRoom.Integration.Tests.Infrastructure
{
    public class TestEvent : IDomainEvent
    {
        [JsonProperty("aggregate_id")]
        public string AggregateId { get; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public TestEvent(string aggregateId, DateTime createdAt, string name)
        {
            AggregateId = aggregateId;
            CreatedAt = createdAt;
            Name = name;
        }
    }
}