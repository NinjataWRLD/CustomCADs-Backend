using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.Accounts.UnitTests.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountLastNameUnitTests : AccountsBaseUnitTests
{
    [Test]
    [TestCase(null)]
    [TestCase("Doe")]
    public void SetLastName_ShouldNotThrowException_WhenLastNameIsValid(string? lastName)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.DoesNotThrow(() =>
        {
            account.SetLastName(lastName);
        });
    }

    [Test]
    public void SetLastName_SetsLastName_WhenLastNameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetLastName(lastName);

        Assert.That(account.LastName, Is.EqualTo(lastName));
    }

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
    public void SetLastName_ThrowsException_WhenLastNameIsInvalid(int lastNameLength)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Throws<AccountValidationException>(() =>
        {
            lastName = new('a', lastNameLength);
            account.SetLastName(lastName);
        });
    }
}
