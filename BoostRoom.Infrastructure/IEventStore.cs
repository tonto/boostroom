using System;
using Tactical.DDD;

namespace BoostRoom.Infrastructure
{
    public interface IEventStore : Tactical.DDD.EventSourcing.IEventStore
    {
        void SubscribeToAll(string subscriptionName, Action<IDomainEvent> action);
    }
}