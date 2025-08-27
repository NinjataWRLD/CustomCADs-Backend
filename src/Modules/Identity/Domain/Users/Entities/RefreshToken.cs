using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Users.Entities;

using static Constants.Tokens;

public class RefreshToken : BaseEntity
{
	private RefreshToken() { }
	private RefreshToken(string value, UserId userId, bool longerSession)
	{
		Value = value;
		UserId = userId;
		IssuedAt = DateTimeOffset.UtcNow;
		ExpiresAt = IssuedAt.AddDays(
			longerSession ? LongerRtDurationInDays : RtDurationInDays
		);
	}
	private RefreshToken(string value, UserId userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
	{
		Value = value;
		UserId = userId;
		IssuedAt = issuedAt;
		ExpiresAt = expiresAt;
	}

	public RefreshTokenId Id { get; init; }
	public DateTimeOffset IssuedAt { get; init; }
	public DateTimeOffset ExpiresAt { get; init; }
	public string Value { get; private set; } = string.Empty;
	public UserId UserId { get; private set; }

	public static RefreshToken Create(string value, UserId userId, bool longerSession)
		=> new(value, userId, longerSession);

	public static RefreshToken Create(RefreshTokenId id, string value, UserId userId, bool longerSession)
		=> new(value, userId, longerSession)
		{
			Id = id,
		};

	public static RefreshToken Create(RefreshTokenId id, string value, UserId userId, DateTimeOffset issuedAt, DateTimeOffset expiresAt)
		=> new(value, userId, issuedAt, expiresAt)
		{
			Id = id,
		};
}
