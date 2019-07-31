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

            //client.Apply(
            //    new ClientRegistered(
            //        id.ToString(),
            //        username,
            //        email,
            //        encryptedPassword,
            //        firstName,
            //        lastName,
            //        addressLine,
            //        city,
            //        zip,
            //        country,
            //        subscribeToOffers)
            //);

            return seller;
        }
    }
}