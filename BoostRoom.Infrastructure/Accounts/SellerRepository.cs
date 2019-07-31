using BoostRoom.Accounts.Domain.SellerAggregate;

namespace BoostRoom.Infrastructure.Accounts
{
    public class SellerRepository : ISellerRepository
    {
        public SellerId NextId()
        {
            return new SellerId();
        }
    }
}