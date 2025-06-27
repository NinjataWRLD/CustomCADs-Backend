using CustomCADs.Identity.Persistence.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Identity.Persistence.Configurations.AppRefreshTokens;

public static class Utilities
{
	public static EntityTypeBuilder<AppRefreshToken> SetValidations(this EntityTypeBuilder<AppRefreshToken> builder)
	{
		builder.Property(u => u.Value)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.Value));

		builder.Property(u => u.IssuedAt)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.IssuedAt));

		builder.Property(u => u.ExpiresAt)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.ExpiresAt));

		builder.Property(u => u.UserId)
			.IsRequired()
			.HasColumnName(nameof(AppRefreshToken.UserId));

		return builder;
	}
}
