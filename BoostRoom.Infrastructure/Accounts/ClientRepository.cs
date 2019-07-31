using BoostRoom.Accounts.Domain.ClientAggregate;

namespace BoostRoom.Infrastructure.Accounts
{
    public class ClientRepository : IClientRepository
    {
        public ClientId NextId()
        {
            return new ClientId();
        }
    }
}