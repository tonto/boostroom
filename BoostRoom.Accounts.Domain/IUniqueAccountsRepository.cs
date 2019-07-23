using System.Threading.Tasks;

namespace BoostRoom.Accounts.Domain
{
    public interface IUniqueAccountsRepository
    {
        Task<bool> AreUnique(string username, string email);
    }
}