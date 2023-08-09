namespace Contracts.Response.Auth
{
    public class AccountSignUpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
