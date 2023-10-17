namespace Account.Contracts.User.Response
{
    public record UserResponse
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
