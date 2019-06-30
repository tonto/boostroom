using System;
using BoostRoom.Accounts.Domain.ClientAggregate;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        public IEntityId AggregateId { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        protected DomainEvent()
        {
            CreatedAt = DateTime.Now;
        }
    }
}