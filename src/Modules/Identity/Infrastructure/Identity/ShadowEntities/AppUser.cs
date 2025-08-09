using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;

public class AppUser : IdentityUser<Guid>
{
	private List<AppRefreshToken> refreshTokens = [];

	public AppUser() : base() { }
	public AppUser(string username, string email, AccountId accountId)
		: base(username)
	{
		Email = email;
		AccountId = accountId;
	}

	public AccountId AccountId { get; set; }
	public IReadOnlyCollection<AppRefreshToken> RefreshTokens => refreshTokens;

	internal AppUser FillRefreshTokens(ICollection<AppRefreshToken> refreshTokens)
	{
		this.refreshTokens = [.. refreshTokens];
		return this;
	}
}
