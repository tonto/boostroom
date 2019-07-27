namespace BoostRoom.Accounts.Domain
{
    public class UsernameEmailTakenException : DomainException
    {
        public UsernameEmailTakenException(string message) : base(message)
        {
        }
    }
}