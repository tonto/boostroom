using System.Threading.Tasks;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public interface IClientRepository
    {
        ClientId NextId();
    }
}