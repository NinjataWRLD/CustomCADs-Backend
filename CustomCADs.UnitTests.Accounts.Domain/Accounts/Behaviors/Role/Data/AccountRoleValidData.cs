namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Role.Data;

using static RolesData;

public class AccountRoleValidData : AccountRoleData
{
    public AccountRoleValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
        Add(ValidName3);
        Add(ValidName4);
    }
}
