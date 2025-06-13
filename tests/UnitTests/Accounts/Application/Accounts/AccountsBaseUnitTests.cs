using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Account CreateAccount(string role = RolesData.ValidName, string username = ValidUsername, string email = ValidEmail1, string? firstName = ValidFirstName, string? lastName = ValidLastName)
		=> Account.Create(role, username, email, firstName, lastName);

	protected static Account CreateAccountWithId(AccountId? id = null, string role = RolesData.ValidName, string username = ValidUsername, string email = ValidEmail1, DateTimeOffset? createdAt = null, string? firstName = ValidFirstName, string? lastName = ValidLastName)
		=> Account.CreateWithId(id ?? ValidId, role, username, email, createdAt ?? DateTimeOffset.UtcNow, firstName, lastName);
}
