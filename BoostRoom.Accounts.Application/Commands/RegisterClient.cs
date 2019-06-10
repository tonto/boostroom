namespace BoostRoom.Accounts.Application.Commands
{
    public sealed class RegisterClient
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string AddressLine { get; }
        public string City { get; }
        public string Zip { get; }
        public string Country { get; }
        public bool SubscribeToOffers { get; }

        public RegisterClient(
            string username,
            string email,
            string password,
            string firstName,
            string lastName,
            string addressLine,
            string city,
            string zip,
            string country,
            bool subscribeToOffers)
        {
            Username = username;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            AddressLine = addressLine;
            City = city;
            Zip = zip;
            Country = country;
            SubscribeToOffers = subscribeToOffers;
        }
    }
}