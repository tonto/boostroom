using System;
using System.Buffers.Text;
using BoostRoom.Accounts.Domain;

namespace BoostRoom.Infrastructure.Accounts
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            return password;
        }

        public string Decode(string password)
        {
            return password;
        }
    }
}