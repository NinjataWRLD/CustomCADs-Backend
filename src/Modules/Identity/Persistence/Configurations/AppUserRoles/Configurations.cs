using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppUserRoles;

using AppUserRole = Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>;

public class Configurations : IEntityTypeConfiguration<AppUserRole>
{
	public void Configure(EntityTypeBuilder<AppUserRole> builder)
		=> builder
			.SetSeeding();
}
