using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Role.Data;

namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Role;

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

        Assert.Throws<CustomValidationException<Account>>(() =>
        {
            account.SetRole(role);
        });
    }
}
