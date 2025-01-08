using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId;

public class AccountCreateWithIdUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenAccountIsValid(AccountId id, string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        CreateAccountWithId(id, role, username, email, timeZone, firstName, lastName);
    }

    [Theory]
    [ClassData(typeof(AccountCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulateCorrectly_WhenAccountIsValid(AccountId id, string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        var account = CreateAccountWithId(id, role, username, email, timeZone, firstName, lastName);

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
    [ClassData(typeof(AccountCreateWithIdInvalidRoleData))]
    [ClassData(typeof(AccountCreateWithIdInvalidUsernameData))]
    [ClassData(typeof(AccountCreateWithIdInvalidEmailData))]
    [ClassData(typeof(AccountCreateWithIdInvalidTimeZoneData))]
    [ClassData(typeof(AccountCreateWithIdInvalidFirstNameData))]
    [ClassData(typeof(AccountCreateWithIdInvalidLastNameData))]
    public void CreateWithId_ShouldThrowException_WhenAccountIsInvalid(AccountId id, string role, string username, string email, string timeZone, string? firstName, string? lastName)
    {
        Assert.Throws<AccountValidationException>(() =>
        {
            CreateAccountWithId(id, role, username, email, timeZone, firstName, lastName);
        });
    }
}
