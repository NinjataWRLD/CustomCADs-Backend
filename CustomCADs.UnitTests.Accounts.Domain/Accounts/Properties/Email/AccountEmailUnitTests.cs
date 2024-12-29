using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Email.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Email;

public class AccountEmailData : TheoryData<string>;

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

        Assert.Throws<AccountValidationException>(() =>
        {
            account.SetEmail(email);
        });
    }
}
