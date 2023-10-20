namespace Account.Contracts.UserRoles.Responses
{
    public class UserRolesResponse
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string Slug { get; set; }
    }
}
