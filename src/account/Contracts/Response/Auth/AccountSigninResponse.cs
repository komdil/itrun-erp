namespace Account.Contracts.Response.Auth
{
    public record AccountSignInResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
