using CustomCADs.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Infrastructure;

public class AuthContext(DbContextOptions<AuthContext> opt) : IdentityDbContext<AppUser, AppRole, Guid>(opt)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Auth");
        builder.ApplyConfigurationsFromAssembly(AuthInfrastructureReference.Assembly);
    }
}
