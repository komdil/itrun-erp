using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.Auth
{
    public class AccountLoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
