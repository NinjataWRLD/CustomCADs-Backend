using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountFirstNameUnitTests : AccountsBaseUnitTests
{
    [Test]
    [TestCase(null)]
    [TestCase("John")]
    public void SetFirstName_ShouldNotThrowException_WhenFirstNameIsValid(string? firstName)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.DoesNotThrow(() =>
        {
            account.SetFirstName(firstName);
        });
    }

    [Test]
    public void SetFirstName_SetsFirstName_WhenFirstNameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetFirstName(firstName);

        Assert.That(account.FirstName, Is.EqualTo(firstName));
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
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
