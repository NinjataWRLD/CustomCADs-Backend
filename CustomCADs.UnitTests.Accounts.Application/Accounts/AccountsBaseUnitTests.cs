namespace CustomCADs.UnitTests.Accounts.Application.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
    public static readonly CancellationToken ct = CancellationToken.None;

    protected static Account CreateAccount(string role = RolesData.ValidName1, string username = ValidUsername1, string email = ValidEmail1, string timeZone = ValidTimeZone1, string? firstName = ValidFirstName1, string? lastName = ValidLastName1)
        => Account.Create(role, username, email, timeZone, firstName, lastName);
}
