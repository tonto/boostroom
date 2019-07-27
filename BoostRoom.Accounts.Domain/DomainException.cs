using System;

namespace BoostRoom.Accounts.Domain
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}