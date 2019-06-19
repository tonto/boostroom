using System.Collections.Generic;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class Address : ValueObject
    {
        public string AddressLine { get; }

        public string City { get; }

        public string Zip { get; }

        public string Country { get; }

        public Address(string addressLine, string city, string zip, string country)
        {
            AddressLine = addressLine;
            City = city;
            Zip = zip;
            Country = country;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return AddressLine;
            yield return City;
            yield return Zip;
            yield return Country;
        }
    }
}