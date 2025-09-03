using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Domain.Users;

public class User : BaseAggregateRoot
{
	private List<RefreshToken> refreshTokens = [];

	private User() { }
	private User(string role, string username, Email email, AccountId accountId) : base()
	{
		Role = role;
		Username = username;
		Email = email;
		AccountId = accountId;
	}

	public UserId Id { get; init; }
	public string Username { get; private set; } = string.Empty;
	public Email Email { get; private set; } = new();
	public string Role { get; private set; } = string.Empty;
	public AccountId AccountId { get; private set; }
	public IReadOnlyCollection<RefreshToken> RefreshTokens => refreshTokens;

	private User FillRefreshTokens(ICollection<RefreshToken> refreshTokens)
	{
		this.refreshTokens = [.. refreshTokens];
		return this;
	}

	public static User Create(string role, string username, Email email, AccountId accountId)
		=> new User(role, username, email, accountId)
			.ValidateRole()
			.ValidateUsername()
			.ValidateEmail();

	public static User Create(UserId id, string role, string username, Email email, AccountId accountId, ICollection<RefreshToken> refreshTokens)
		=> new User(role, username, email, accountId)
		{
			Id = id,
		}
		.FillRefreshTokens(refreshTokens)
		.ValidateRole()
		.ValidateUsername()
		.ValidateEmail();

	public void SetUsername(string username)
	{
		Username = username;
		this.ValidateUsername();
	}

	public RefreshToken AddRefreshToken(string token, bool longerSession)
	{
		RefreshToken rt = RefreshToken.Create(token, this.Id, longerSession);
		refreshTokens.Add(rt);

		return rt;
	}

	public bool RemoveRefreshToken(RefreshToken rt)
	{
		return refreshTokens.Remove(rt);
	}
}
