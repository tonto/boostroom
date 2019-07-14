using BoostRoom.Accounts.Domain.ClientAggregate;

namespace BoostRoom.Infrastructure.Accounts
{
    public class ClientsRepository : IClientsRepository
    {
        public ClientId NextId()
        {
            return new ClientId();
        }
    }
}