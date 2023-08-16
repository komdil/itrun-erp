namespace Contracts.Response.Auth
{
    public record AccountSigninResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
