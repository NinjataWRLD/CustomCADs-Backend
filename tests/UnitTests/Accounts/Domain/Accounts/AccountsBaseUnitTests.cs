using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
	protected static Account CreateAccount(
		string role = RolesData.ValidName,
		string username = ValidUsername,
		string email = ValidEmail1,
		string? firstName = ValidFirstName,
		string? lastName = ValidLastName
	) => Account.Create(
			role: role,
			username: username,
			email: email,
			firstName: firstName,
			lastName: lastName
		);

	protected static Account CreateAccountWithId(
		AccountId? id = null,
		string role = RolesData.ValidName,
		string username = ValidUsername,
		string email = ValidEmail1,
		DateTimeOffset? createdAt = null,
		string? firstName = ValidFirstName,
		string? lastName = ValidLastName
	) => Account.CreateWithId(
			id: id ?? ValidId,
			role: role,
			username: username,
			email: email,
			createdAt: createdAt ?? DateTimeOffset.UtcNow,
			firstName: firstName,
			lastName: lastName
		);
}
