using System.Threading.Tasks;

namespace BoostRoom.Accounts.Domain
{
    public interface IUniqueAccountsProjection
    {
        Task<bool> AreUnique(string username, string email);
    }
}