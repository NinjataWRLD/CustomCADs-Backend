using CustomCADs.Accounts.Domain.Common.Exceptions.Accounts;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Role.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Properties.Role;

public class AccountRoleData : TheoryData<string>;

public class AccountRoleUnitTests : AccountsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(AccountRoleValidData))]
    public void SetRole_ShouldNotThrowException_WhenRoleIsValid(string role)
    {
        var account = CreateAccount();

        account.SetRole(role);
    }

    [Theory]
    [ClassData(typeof(AccountRoleValidData))]
    public void SetRole_SetsRole_WhenRoleIsValid(string role)
    {
        var account = CreateAccount();

        account.SetRole(role);

        Assert.Equal(account.RoleName, role);
    }

    [Theory]
    [ClassData(typeof(AccountRoleInvalidData))]
    public void SetRole_ThrowsException_WhenRoleIsInvalid(string role)
    {
        var account = CreateAccount();

        Assert.Throws<AccountValidationException>(() =>
        {
            account.SetRole(role);
        });
    }
}
