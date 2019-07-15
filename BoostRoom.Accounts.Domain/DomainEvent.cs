using System;
using BoostRoom.Accounts.Domain.ClientAggregate;
using Newtonsoft.Json;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        [JsonProperty("aggregate_id")]
        public string AggregateId { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        protected DomainEvent()
        {
        }
    }
}