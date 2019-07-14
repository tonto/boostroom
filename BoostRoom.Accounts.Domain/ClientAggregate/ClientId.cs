using System;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class ClientId : EntityId
    {
        private readonly Guid _id;

        public ClientId()
        {
            _id = Guid.NewGuid();
        }

        public ClientId(string id)
        {
            _id = Guid.Parse(id);
        }

        public override string ToString() => _id.ToString();
    }
}