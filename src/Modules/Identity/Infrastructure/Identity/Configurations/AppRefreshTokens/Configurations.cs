using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Infrastructure.Identity.Configurations.AppRefreshTokens;

public class Configurations : IEntityTypeConfiguration<AppRefreshToken>
{
	public void Configure(EntityTypeBuilder<AppRefreshToken> builder)
		=> builder
			.SetValidations();
}
