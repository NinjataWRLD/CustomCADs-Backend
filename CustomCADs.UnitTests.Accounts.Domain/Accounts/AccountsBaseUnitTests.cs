namespace CustomCADs.UnitTests.Accounts.Domain.Accounts;

using static AccountsData;

public class AccountsBaseUnitTests
{
    protected static Account CreateAccount(
        string role = RolesData.ValidName1,
        string username = ValidUsername1,
        string email = ValidEmail1,
        string timeZone = ValidTimeZone1,
        string? firstName = ValidFirstName1,
        string? lastName = ValidLastName1
    ) => Account.Create(
            role: role,
            username: username,
            email: email,
            timeZone: timeZone,
            firstName: firstName,
            lastName: lastName
        );
}
