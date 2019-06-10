namespace BoostRoom.Accounts.Domain
{
    public abstract class User
    {
        public Username Username { get; private set; }
        
        public Email Email { get; private set; }

        private Password Password;
    }
}