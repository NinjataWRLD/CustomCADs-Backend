using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.LastName;

using Data;

public class AccountLastNameUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountLastNameValidData))]
    public void SetLastName_ShouldNotThrowException_WhenLastNameIsValid(string? lastName)
    {
        var account = CreateAccount();

        account.SetLastName(lastName);
    }

    [Theory]
    [ClassData(typeof(AccountLastNameValidData))]
    public void SetLastName_SetsLastName_WhenLastNameIsValid(string? lastName)
    {
        var account = CreateAccount();

        account.SetLastName(lastName);

        Assert.Equal(account.LastName, lastName);
    }

    [Theory]
    [ClassData(typeof(AccountLastNameInvalidData))]
    public void SetLastName_ThrowsException_WhenLastNameIsInvalid(string? lastName)
    {
        var account = CreateAccount();

        Assert.Throws<CustomValidationException<Account>>(() =>
        {
            account.SetLastName(lastName);
        });
    }
}
