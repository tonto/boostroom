namespace BoostRoom.Accounts.Domain.SellerAggregate
{
    public interface ISellerRepository
    {
        SellerId NextId();
    }
}