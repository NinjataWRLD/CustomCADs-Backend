using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Persistence.ShadowEntities;

public class AppUser : IdentityUser<Guid>
{
	public AppUser() : base() { }

	public AppUser(string username, string email, AccountId accountId)
		: base(username)
	{
		Email = email;
		AccountId = accountId;
	}

	public AccountId AccountId { get; set; }
	public RefreshToken? RefrehToken { get; set; }
}
