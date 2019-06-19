using System.Collections.Generic;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public class FullName : ValueObject
    {
        public string FirstName { get; }
        
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}