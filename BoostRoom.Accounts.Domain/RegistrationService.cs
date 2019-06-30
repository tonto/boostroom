namespace BoostRoom.Accounts.Domain
{
    public sealed class RegistrationService
    {
        private IPasswordEncoder _passwordEncoder;

        public RegistrationService(IPasswordEncoder passwordEncoder)
        {
            _passwordEncoder = passwordEncoder;
        }

        public void RegisterClient(
            string username,
            string email,
            string password,
            string firstName,
            string lastName,
            string address,
            string city,
            string zip,
            string country,
            bool subscribeToOffers
        )
        {
            // Where do we do password, email, username validation ???
            
            // a value object can enforce some structural validations that itself is
            // responsible for, eg. email form, price not being negative, non null values,
            // zip structure (?) etc... - but other stuff that's enforced as a application or
            // company level policy, eg. password policy, username policy should be enforced elsewhere 
            // (maybe domain services or, validation)
            
            // Check if username / email unique 
            // Encrypt password
            // Instantiate client aggregate
            // Persist
        }
    }
}