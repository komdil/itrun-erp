using System.ComponentModel.DataAnnotations;

namespace Account.Contracts.Auth
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
