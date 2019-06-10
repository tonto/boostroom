namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public class FullName
    {
        public string FirstName { get; }
        
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}