using System.ComponentModel.DataAnnotations;
using MediatR;

namespace BoostRoom.Accounts.Application.Commands
{
    public class RegisterSeller : IRequest
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
        public string Country { get; set; }
    }
}