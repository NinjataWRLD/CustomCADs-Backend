using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.Normal;

using Data;

public class AccountCreateUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountCreateValidData))]
    public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
    {
        CreateAccount(role, username, email, firstName, lastName);
    }

    [Theory]
    [ClassData(typeof(AccountCreateValidData))]
    public void Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string email, string? firstName, string? lastName)
    {
        var account = CreateAccount(role, username, email, firstName, lastName);

        Assert.Multiple(
            () => Assert.Equal(role, account.RoleName),
            () => Assert.Equal(username, account.Username),
            () => Assert.Equal(email, account.Email),
            () => Assert.Equal(firstName, account.FirstName),
            () => Assert.Equal(lastName, account.LastName)
        );
    }

    [Theory]
    [ClassData(typeof(AccountCreateInvalidRoleData))]
    [ClassData(typeof(AccountCreateInvalidUsernameData))]
    [ClassData(typeof(AccountCreateInvalidEmailData))]
    [ClassData(typeof(AccountCreateInvalidFirstNameData))]
    [ClassData(typeof(AccountCreateInvalidLastNameData))]
    public void Create_ShouldThrowException_WhenAccountIsInvalid(string role, string username, string email, string? firstName, string? lastName)
    {
        Assert.Throws<CustomValidationException<Account>>(() =>
        {
            CreateAccount(role, username, email, firstName, lastName);
        });
    }
}
