namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Properties.Description.Data;

using static RolesData;

public class RoleCreateValidData : RoleDescriptionData
{
    public RoleCreateValidData()
    {
        Add(ValidDescription1);
        Add(ValidDescription2);
        Add(ValidDescription3);
        Add(ValidDescription4);
    }
}
