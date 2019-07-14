using System.Collections.Generic;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;

namespace BoostRoom.Infrastructure.Accounts
{
    public class UniqueAccountsProjection : IUniqueAccountsProjection
    {
        // TODO - Persist this projection to db

        private List<AccountEntry> _accounts = new List<AccountEntry>(); 

        public Task<bool> AreUnique(string username, string email)
        {
            return Task.FromResult(true);
        }

        private class AccountEntry
        {
            public string Username { get; set; }

            public string Email { get; set; }
        }
    }
}