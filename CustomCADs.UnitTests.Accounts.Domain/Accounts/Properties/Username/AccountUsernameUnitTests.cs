using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Username.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Username;

public class AccountUsernameData : TheoryData<string>;

public class AccountUsernameUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountUsernameValidData))]
    public void SetUsername_ShouldNotThrowException_WhenUsernameIsValid(string username)
    {
        var account = CreateAccount();

        account.SetUsername(username);
    }

    [Theory]
    [ClassData(typeof(AccountUsernameValidData))]
    public void SetUsername_SetsUsername_WhenUserameIsValid(string username)
    {
        var account = CreateAccount();
        
        account.SetUsername(username);

        Assert.Equal(account.Username, username);
    }

    [Theory]
    [ClassData(typeof(AccountUsernameInvalidData))]
    public void SetUsername_ThrowsException_WhenUserameIsInvalid(string username)
    {
        var account = CreateAccount();

        Assert.Throws<AccountValidationException>(() =>
        {
            account.SetUsername(username);
        });
    }
}
