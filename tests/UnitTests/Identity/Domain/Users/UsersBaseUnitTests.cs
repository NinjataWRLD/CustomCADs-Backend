using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Identity.Domain.Users;

using static UsersData;

public class UsersBaseUnitTests
{
	public static User CreateUser(
		string? role = null,
		string? username = null,
		string? email = null,
		AccountId? accountId = null
	) => User.Create(
			role ?? ValidRole,
			username ?? MinValidUsername,
			new(email ?? ValidEmail, false),
			accountId ?? ValidAccountId
		);

	public static User CreateUserWithId(
		UserId? id = null,
		string? role = null,
		string? username = null,
		string? email = null,
		AccountId? accountId = null,
		RefreshToken[]? refreshTokens = null
	) => User.Create(
			id ?? ValidId,
			role ?? ValidRole,
			username ?? MinValidUsername,
			new(email ?? ValidEmail, false),
			accountId ?? ValidAccountId,
			refreshTokens: refreshTokens ?? []
		);
}
