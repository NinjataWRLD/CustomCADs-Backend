using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.Shared.Core;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

using static AccountConstants;
using static Constants.Roles;

public class AccountCreateUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [InlineData(Client, "J0HN_D03", null, null)]
    [InlineData(Contributor, "J0HN_D03", "John", null)]
    [InlineData(Designer, "J0HN_D03", null, "Doe")]
    [InlineData(Admin, "J0HN_D03", "John", "Doe")]
    public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string? firstName, string? lastName)
    {
        Account.Create(role, username, Email, TimeZone, firstName, lastName);
    }

    [Theory]
    [InlineData(Client, "J0HN_D03", null, null)]
    [InlineData(Contributor, "J0HN_D03", "John", null)]
    [InlineData(Designer, "J0HN_D03", null, "Doe")]
    [InlineData(Admin, "J0HN_D03", "John", "Doe")]
    public void Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string? firstName, string? lastName)
    {
        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Multiple(
            () => Assert.Equal(role, account.RoleName),
            () => Assert.Equal(username, account.Username),
            () => Assert.Equal(Email, account.Email),
            () => Assert.Equal(TimeZone, account.TimeZone),
            () => Assert.Equal(firstName, account.FirstName),
            () => Assert.Equal(lastName, account.LastName)
        );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(RoleConstants.NameMinLength - 1)]
    [InlineData(RoleConstants.NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenRoleIsInvalid(int roleLength)
    {
        string role = new('a', roleLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        Assert.Throws<AccountValidationException>(() =>
        {
            Account.Create(role, username, Email, TimeZone, firstName, lastName);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenUsernameIsInvalid(int usernameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', usernameLength);
        string? firstName = null;
        string? lastName = null;

        Assert.Throws<AccountValidationException>(() =>
        {
            Account.Create(role, username, Email, TimeZone, firstName, lastName);
        });
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("@a")]
    [InlineData("a@a")]
    [InlineData("a@a.a")]
    [InlineData(" a@a.co")]
    [InlineData("a@a.co ")]
    public void Create_ShouldThrowException_WhenEmailIsInvalid(string email)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        Assert.Throws<AccountValidationException>(() =>
        {
            Account.Create(role, username, email, TimeZone, firstName, lastName);
        });
    }

    [Theory]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenFirstNameIsInvalid(int firstNameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = new('a', firstNameLength);
        string? lastName = null;

        Assert.Throws<AccountValidationException>(() =>
        {
            Account.Create(role, username, Email, TimeZone, firstName, lastName);
        });
    }

    [Theory]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void Create_ShouldThrowException_WhenLastNameIsInvalid(int lastNameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = new('a', lastNameLength);

        Assert.Throws<AccountValidationException>(() =>
        {
            Account.Create(role, username, Email, TimeZone, firstName, lastName);
        });
    }
}
