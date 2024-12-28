using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountRoleUnitTests : AccountsBaseUnitTests
{
    [Test]
    public void SetRole_ShouldNotThrowException_WhenRoleIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.DoesNotThrow(() =>
        {
            account.SetRole(role);
        });
    }

    [Test]
    public void SetRole_SetsRole_WhenRoleIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetRole(role);

        Assert.That(account.RoleName, Is.EqualTo(role));
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
    public void SetRole_ThrowsException_WhenRoleIsInvalid(int roleLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Throws<AccountValidationException>(() =>
        {
            role = new('a', roleLength);
            account.SetRole(role);
        });
    }
}
