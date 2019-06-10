namespace BoostRoom.Accounts.Domain
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string email)
        {
            Value = email;
        }
    }
}