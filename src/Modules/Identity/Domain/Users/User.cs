using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Users;

public class User
{
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
	public RefreshToken? RefreshToken { get; private set; }
	public AccountId AccountId { get; private set; }

	public static User Create(string role, string username, Email email, AccountId accountId)
		=> new User(role, username, email, accountId)
			.ValidateUsername()
			.ValidateEmail();

	public static User Create(UserId id, string role, string username, Email email, RefreshToken? refreshToken, AccountId accountId)
		=> new User(role, username, email, accountId)
		{
			Id = id,
			RefreshToken = refreshToken,
		}
		.ValidateUsername()
		.ValidateEmail();

	public void SetRefreshToken(string token, DateTimeOffset expiresAt)
	{
		RefreshToken = new(token, expiresAt);
	}

	public void EraseRefreshToken()
	{
		RefreshToken = null;
	}
}
