using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.Shared.Core;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create;

using static AccountConstants;
using static Constants.Roles;

public class AccountCreateUnitTests : AccountsBaseUnitTests
{
    [Test]
    [TestCase(Client, "J0HN_D03", null, null)]
    [TestCase(Contributor, "J0HN_D03", "John", null)]
    [TestCase(Designer, "J0HN_D03", null, "Doe")]
    [TestCase(Admin, "J0HN_D03", "John", "Doe")]
    public void Create_ShouldNotThrowException_WhenAccountIsValid(string role, string username, string? firstName, string? lastName)
    {
        Assert.DoesNotThrow(() =>
        {
            Account.Create(role, username, Email, TimeZone, firstName, lastName);
        });
    }

    [Test]
    [TestCase(Client, "J0HN_D03", null, null)]
    [TestCase(Contributor, "J0HN_D03", "John", null)]
    [TestCase(Designer, "J0HN_D03", null, "Doe")]
    [TestCase(Admin, "J0HN_D03", "John", "Doe")]
    public void Create_ShouldPopulateCorrectly_WhenAccountIsValid(string role, string username, string? firstName, string? lastName)
    {
        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        Assert.Multiple(() =>
        {
            Assert.That(account.RoleName, Is.EqualTo(role));
            Assert.That(account.Username, Is.EqualTo(username));
            Assert.That(account.Email, Is.EqualTo(Email));
            Assert.That(account.TimeZone, Is.EqualTo(TimeZone));
            Assert.That(account.FirstName, Is.EqualTo(firstName));
            Assert.That(account.LastName, Is.EqualTo(lastName));
        });
    }

    [Test]
    [TestCase(0)]
    [TestCase(RoleConstants.NameMinLength - 1)]
    [TestCase(RoleConstants.NameMaxLength + 1)]
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

    [Test]
    [TestCase(0)]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
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

    [Test]
    [TestCase(null!)]
    [TestCase("a")]
    [TestCase("@a")]
    [TestCase("a@a")]
    [TestCase("a@a.a")]
    [TestCase(" a@a.co")]
    [TestCase("a@a.co ")]
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

    [Test]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
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

    [Test]
    [TestCase(NameMinLength - 1)]
    [TestCase(NameMaxLength + 1)]
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
