using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Identity.Application.Users;

using static UsersData;

public class UsersBaseUnitTests
{
	protected private readonly CancellationToken ct = default;

	protected static User CreateUser(
		string? role = null,
		string? username = null,
		Email? email = null,
		AccountId? accountId = null
	) => User.Create(
			role ?? ValidRole,
			username ?? MinValidUsername,
			email ?? new(ValidEmail, true),
			accountId ?? ValidAccountId
		);

	protected static User CreateUserWithId(
		UserId? id = null,
		string? role = null,
		string? username = null,
		Email? email = null,
		AccountId? accountId = null,
		RefreshToken[]? refreshTokens = null
	) => User.Create(
			id ?? ValidId,
			role ?? ValidRole,
			username ?? MinValidUsername,
			email ?? new(ValidEmail, true),
			accountId ?? ValidAccountId,
			refreshTokens: refreshTokens ?? []
		);
}
