namespace Account.Contracts.Auth
{
    public record AccountSignUpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
