using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountFirstNameUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("John")]
    public void SetFirstName_ShouldNotThrowException_WhenFirstNameIsValid(string? firstName)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        account.SetFirstName(firstName);
    }

    [Fact]
    public void SetFirstName_SetsFirstName_WhenFirstNameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetFirstName(firstName);

        Assert.Equal(account.FirstName, firstName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
    public void SetFirstName_ThrowsException_WhenFirstNameIsInvalid(int firstNameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Throws<AccountValidationException>(() =>
        {
            firstName = new('a', firstNameLength);
            account.SetFirstName(firstName);
        });
    }
}
