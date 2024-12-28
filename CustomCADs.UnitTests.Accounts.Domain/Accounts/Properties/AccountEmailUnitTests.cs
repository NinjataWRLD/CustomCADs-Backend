using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountEmailUnitTests : AccountsBaseUnitTests
{
    [Test]
    public void SetEmail_ShouldNotThrowException_WhenEmailIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.DoesNotThrow(() =>
        {
            account.SetEmail(Email);
        });
    }

    [Test]
    public void SetEmail_SetsEmail_WhenEmailIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetEmail(Email);

        Assert.That(account.Email, Is.EqualTo(Email));
    }

    [Test]
    [TestCase(null!)]
    [TestCase("a")]
    [TestCase("@a")]
    [TestCase("a@a")]
    [TestCase("a@a.a")]
    [TestCase(" a@a.co")]
    [TestCase("a@a.co ")]
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
