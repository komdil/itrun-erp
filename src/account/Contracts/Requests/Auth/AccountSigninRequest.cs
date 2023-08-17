using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Auth
{
    public record AccountSignInRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
