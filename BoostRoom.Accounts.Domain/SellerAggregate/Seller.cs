using BoostRoom.Accounts.Domain.ClientAggregate;
using Tactical.DDD.EventSourcing;

namespace BoostRoom.Accounts.Domain.SellerAggregate
{
    public sealed class Seller : AggregateRoot<SellerId>
    {
        private Seller()
        {
        }

        public static Seller FromDetails(
                SellerId id,
                string username,
                string email,
                string country,
                string encryptedPassword)
        {
            var seller = new Seller();

            seller.Apply(
                new SellerRegistered(
                    id.ToString(),
                    username,
                    email,
                    encryptedPassword,
                    country
                )
            );

            return seller;
        }

        public void On(SellerRegistered @event)
        {
            Id = new SellerId(@event.AggregateId);
        }
    }
}