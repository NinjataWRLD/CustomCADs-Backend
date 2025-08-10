using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
	protected static Account CreateAccount(
		string? role = null,
		string username = ValidUsername,
		string email = ValidEmail1,
		string? firstName = ValidFirstName,
		string? lastName = ValidLastName
	) => Account.Create(
			role: role ?? RolesData.ValidName,
			username: username,
			email: email,
			firstName: firstName,
			lastName: lastName
		);

	protected static Account CreateAccountWithId(
		AccountId? id = null,
		string? role = null,
		string username = ValidUsername,
		string email = ValidEmail1,
		DateTimeOffset? createdAt = null,
		string? firstName = ValidFirstName,
		string? lastName = ValidLastName
	) => Account.CreateWithId(
			id: id ?? ValidId,
			role: role ?? RolesData.ValidName,
			username: username,
			email: email,
			createdAt: createdAt ?? DateTimeOffset.UtcNow,
			firstName: firstName,
			lastName: lastName
		);
}
