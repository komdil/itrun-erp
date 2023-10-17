using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public interface IApplicationDbContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationUserClaim> UserClaims { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
    }
}
