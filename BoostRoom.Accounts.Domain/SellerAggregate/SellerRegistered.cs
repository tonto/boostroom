using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BoostRoom.Accounts.Domain.SellerAggregate
{
    public sealed class SellerRegistered : DomainEvent
    {
        [JsonProperty("username")]
        public string Username { get; }

        [JsonProperty("email")]
        public string Email { get; }

        [JsonProperty("encrypted_password")]
        public string EncryptedPassword { get; }

        [JsonProperty("country")]
        public string Country { get; }

        public SellerRegistered(
            string sellerId,
            string username,
            string email,
            string encryptedPassword,
            string country)
        {
            AggregateId = sellerId;
            Username = username;
            Email = email;
            EncryptedPassword = encryptedPassword;
            Country = country;
        }
    }
}
