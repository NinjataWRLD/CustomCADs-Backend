using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountEmailUnitTests : AccountsBaseUnitTests
{
    [Fact]
    public void SetEmail_ShouldNotThrowException_WhenEmailIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        account.SetEmail(Email);
    }

    [Fact]
    public void SetEmail_SetsEmail_WhenEmailIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetEmail(Email);

        Assert.Equal(Email, account.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("@a")]
    [InlineData("a@a")]
    [InlineData("a@a.a")]
    [InlineData(" a@a.co")]
    [InlineData("a@a.co ")]
    public void SetEmail_ThrowsException_WhenEmailIsInvalid(string email)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Throws<AccountValidationException>(() =>
        {
            account.SetEmail(email);
        });
    }
}
