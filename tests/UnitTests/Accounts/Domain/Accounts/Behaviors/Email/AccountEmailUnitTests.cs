using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Email;

using Data;

public class AccountEmailUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountEmailValidData))]
    public void SetEmail_ShouldNotThrowException_WhenEmailIsValid(string email)
    {
        var account = CreateAccount();

        account.SetEmail(email);
    }

    [Theory]
    [ClassData(typeof(AccountEmailValidData))]
    public void SetEmail_Setsemail_WhenUserameIsValid(string email)
    {
        var account = CreateAccount();

        account.SetEmail(email);

        Assert.Equal(account.Email, email);
    }

    [Theory]
    [ClassData(typeof(AccountEmailInvalidData))]
    public void SetEmail_ThrowsException_WhenUserameIsInvalid(string email)
    {
        var account = CreateAccount();

        Assert.Throws<CustomValidationException<Account>>(() =>
        {
            account.SetEmail(email);
        });
    }
}
