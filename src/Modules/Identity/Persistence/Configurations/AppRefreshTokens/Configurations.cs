using CustomCADs.Identity.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppRefreshTokens;

public class Configurations : IEntityTypeConfiguration<AppRefreshToken>
{
	public void Configure(EntityTypeBuilder<AppRefreshToken> builder)
		=> builder
			.SetValidations();
}
