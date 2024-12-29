using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

public class AccountCreateData : TheoryData<string, string, string, string, string?, string?>;

public class AccountCreateUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountCreateValidData))]
    public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        CreateAccount(role, username, email, timeZone, firstName, lastName);
    }

    [Theory]
    [ClassData(typeof(AccountCreateValidData))]
    public void Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        var account = CreateAccount(role, username, email, timeZone, firstName, lastName);

        Assert.Multiple(
            () => Assert.Equal(role, account.RoleName),
            () => Assert.Equal(username, account.Username),
            () => Assert.Equal(email, account.Email),
            () => Assert.Equal(timeZone, account.TimeZone),
            () => Assert.Equal(firstName, account.FirstName),
            () => Assert.Equal(lastName, account.LastName)
        );
    }

    [Theory]
    [ClassData(typeof(AccountCreateInvalidRoleData))]
    [ClassData(typeof(AccountCreateInvalidUsernameData))]
    [ClassData(typeof(AccountCreateInvalidEmailData))]
    [ClassData(typeof(AccountCreateInvalidTimeZoneData))]
    [ClassData(typeof(AccountCreateInvalidFirstNameData))]
    [ClassData(typeof(AccountCreateInvalidLastNameData))]
    public void Create_ShouldThrowException_WhenAccountIsInvalid(string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        Assert.Throws<AccountValidationException>(() =>
        {
            CreateAccount(role, username, email, timeZone, firstName, lastName);
        });
    }
}
