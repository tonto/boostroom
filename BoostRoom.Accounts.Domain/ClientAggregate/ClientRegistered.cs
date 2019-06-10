namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class ClientRegistered
    {
        public string Username { get; }
        public string Email { get; }
        public string EncryptedPassword { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string AddressLine { get; }
        public string City { get; }
        public string Zip { get; }
        public string Country { get; }
        public bool SubscribedToOffers { get; }

        public ClientRegistered(
            string username,
            string email,
            string encryptedPassword,
            string firstName,
            string lastName,
            string addressLine,
            string city,
            string zip,
            string country,
            bool subscribedToOffers)
        {
            Username = username;
            Email = email;
            EncryptedPassword = encryptedPassword;
            FirstName = firstName;
            LastName = lastName;
            AddressLine = addressLine;
            City = city;
            Zip = zip;
            Country = country;
            SubscribedToOffers = subscribedToOffers;
        }
    }
}