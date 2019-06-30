using System.ComponentModel.DataAnnotations;

namespace BoostRoom.WebApp.Models
{
    public class RegisterClientDto
    {
        [Required]
        [MinLength(5)]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(512)]
        public string AddressLine { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        [MaxLength(255)]
        public string Zip { get; set; }

        [Required]
        [MaxLength(255)]
        public string Country { get; set; }

        [Required]
        public bool SubscribeToOffers { get; set; }
    }
}