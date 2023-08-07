using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Auth
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string OrganizationName { get; set; }
    }
}
