using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;

namespace BoostRoom.Infrastructure.Accounts.RavenDB
{
    public class UniqueAccountsRepository : IUniqueAccountsRepository
    {
        // TODO - Inject document store
        
        public Task<bool> AreUnique(string username, string email)
        {
            return Task.FromResult(false);
        }
    }
}