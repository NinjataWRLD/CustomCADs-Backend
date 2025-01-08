namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Role.Data;

using static RolesData;

public class AccountRoleInvalidData : AccountRoleData
{
    public AccountRoleInvalidData()
    {
        Add(InvalidName1);
        Add(InvalidName2);
        Add(InvalidName3);
    }
}
