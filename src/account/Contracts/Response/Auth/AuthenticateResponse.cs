namespace Contracts.Response.Auth
{
    public class AccountLoginResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
