using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
	protected static Account CreateAccount(
		string role = RolesData.ValidName1,
		string username = ValidUsername1,
		string email = ValidEmail1,
		string? firstName = ValidFirstName1,
		string? lastName = ValidLastName1
	) => Account.Create(
			role: role,
			username: username,
			email: email,
			firstName: firstName,
			lastName: lastName
		);

	protected static Account CreateAccountWithId(
		AccountId? id = null,
		string role = RolesData.ValidName1,
		string username = ValidUsername1,
		string email = ValidEmail1,
		string? firstName = ValidFirstName1,
		string? lastName = ValidLastName1
	) => Account.CreateWithId(
			id: id ?? ValidId1,
			role: role,
			username: username,
			email: email,
			firstName: firstName,
			lastName: lastName
		);
}
