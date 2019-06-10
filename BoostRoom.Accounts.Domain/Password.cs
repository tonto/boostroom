namespace BoostRoom.Accounts.Domain
{
    public sealed class Password
    {
        public string Encrypted { get; }

        public Password(string password)
        {
            Encrypted = password;
        }
    }
}