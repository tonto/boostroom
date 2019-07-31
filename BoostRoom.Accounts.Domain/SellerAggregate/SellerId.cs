using System;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain.SellerAggregate
{
    public sealed class SellerId : EntityId
    {
          private readonly Guid _id;

        public SellerId()
        {
            _id = Guid.NewGuid();
        }

        public SellerId(string id)
        {
            _id = Guid.Parse(id);
        }

        public override string ToString() => _id.ToString();
    }
}