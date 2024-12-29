using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties;

using static AccountConstants;

public class AccountLastNameUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("Doe")]
    public void SetLastName_ShouldNotThrowException_WhenLastNameIsValid(string? lastName)
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);

        account.SetLastName(lastName);
    }

    [Fact]
    public void SetLastName_SetsLastName_WhenLastNameIsValid()
    {
        string role = new('a', RoleConstants.NameMinLength);
        string username = new('a', NameMinLength);
        string? firstName = null;
        string? lastName = null;

        var account = Account.Create(role, username, Email, TimeZone, firstName, lastName);
        account.SetLastName(lastName);

        Assert.Equal(account.LastName, lastName);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(NameMinLength - 1)]
    [InlineData(NameMaxLength + 1)]
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
