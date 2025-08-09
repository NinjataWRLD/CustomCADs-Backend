using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Infrastructure.Identity.Configurations.AppUsers;

public class Configurations : IEntityTypeConfiguration<AppUser>
{
	public void Configure(EntityTypeBuilder<AppUser> builder)
		=> builder
			.SetStronglyTypedIds()
			.SetValidations()
			.SetSeeding();
}
