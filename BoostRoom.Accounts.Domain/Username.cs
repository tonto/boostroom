namespace BoostRoom.Accounts.Domain
{
    public sealed class Username
    {
        public string Value { get; }

        public Username(string username)
        {
            Value = username;
        }
    }
}