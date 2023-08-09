using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Auth
{
    public class AccountSignInRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
