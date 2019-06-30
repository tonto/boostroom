namespace BoostRoom.Accounts.Domain
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
    }
}