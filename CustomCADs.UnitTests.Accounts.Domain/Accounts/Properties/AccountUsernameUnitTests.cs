using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountUsernameUnitTests : AccountsBaseUnitTests
{
    [Test]
    public void SetUsername_ShouldNotThrowException_WhenUsernameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.DoesNotThrow(() =>
        {
            account.SetUsername(username);
        });
    }

    [Test]
    public void SetUsername_SetsUsername_WhenUserameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetUsername(username);

        Assert.That(account.Username, Is.EqualTo(username));
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
    public void SetUsername_ThrowsException_WhenUserameIsInvalid(int usernameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Throws<AccountValidationException>(() =>
        {
            username = new('a', usernameLength);
            account.SetUsername(username);
        });
    }
}
