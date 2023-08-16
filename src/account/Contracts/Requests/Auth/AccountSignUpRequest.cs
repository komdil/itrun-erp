using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Auth
{
    public record AccountSignUpRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string OrganizationName { get; set; }
    }
}
