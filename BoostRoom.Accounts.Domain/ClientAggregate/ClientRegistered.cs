using System;
using Newtonsoft.Json;
using Tactical.DDD;

namespace BoostRoom.Accounts.Domain.ClientAggregate
{
    public sealed class ClientRegistered : DomainEvent
    {
        [JsonProperty("username")]
        public string Username { get; }
        
        [JsonProperty("email")]
        public string Email { get; }
        
        [JsonProperty("encrypted_password")]
        public string EncryptedPassword { get; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; }
        
        [JsonProperty("last_name")]
        public string LastName { get; }
        
        [JsonProperty("address_line")]
        public string AddressLine { get; }
        
        [JsonProperty("city")]
        public string City { get; }
        
        [JsonProperty("zip")]
        public string Zip { get; }
        
        [JsonProperty("country")]
        public string Country { get; }
        
        [JsonProperty("subscribed_to_offers")]
        public bool SubscribedToOffers { get; }

        public ClientRegistered(
            string clientId,
            string username,
            string email,
            string encryptedPassword,
            string firstName,
            string lastName,
            string addressLine,
            string city,
            string zip,
            string country,
            bool subscribedToOffers)
        {
            AggregateId = clientId;
            Username = username;
            Email = email;
            EncryptedPassword = encryptedPassword;
            FirstName = firstName;
            LastName = lastName;
            AddressLine = addressLine;
            City = city;
            Zip = zip;
            Country = country;
            SubscribedToOffers = subscribedToOffers;
        }
    }
}