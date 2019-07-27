using System.Linq;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;
using Raven.Client.Documents;

namespace BoostRoom.Infrastructure.Accounts.RavenDB
{
    public class UniqueAccountsRepository : IUniqueAccountsRepository
    {
        private readonly IDocumentStore _documentStore;

        public UniqueAccountsRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public async Task<bool> AreUnique(string username, string email)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                var results = await session.Query<UniqueAccountsProjection.AccountEntry>()
                    .Where(a => a.Email == email || a.Username == username)
                    .ToListAsync();

                if (results == null) return true;

                return !results.Any();
            }
        }
    }
}