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

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        protected DomainEvent()
        {
            CreatedAt = DateTime.Now;
        }
    }
}