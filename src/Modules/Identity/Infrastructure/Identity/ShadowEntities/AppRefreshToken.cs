using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;

public class AppRefreshToken : BaseEntity
{
	public AppRefreshToken() { }
	public AppRefreshToken(string value, Guid userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
	{
		Value = value;
		UserId = userId;
		IssuedAt = issuedAt;
		ExpiresAt = expiresAt;
	}

	public Guid Id { get; init; }
	public DateTimeOffset IssuedAt { get; init; }
	public DateTimeOffset ExpiresAt { get; init; }
	public string Value { get; private set; } = string.Empty;
	public Guid UserId { get; private set; }
	public AppUser User { get; init; } = null!;
}
