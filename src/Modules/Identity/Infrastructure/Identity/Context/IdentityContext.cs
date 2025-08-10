using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Infrastructure.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> opt) : IdentityDbContext<AppUser, AppRole, Guid>(opt)
{
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.HasDefaultSchema("Identity");
		builder.ApplyConfigurationsFromAssembly(IdentityPersistenceReference.Assembly);
	}
}
