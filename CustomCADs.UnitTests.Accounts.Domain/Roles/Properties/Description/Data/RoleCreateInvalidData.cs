namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Properties.Description.Data;

using static RolesData;

public class RoleCreateInvalidData : RoleDescriptionData
{
    public RoleCreateInvalidData()
    {
        Add(InvalidDescription1);
        Add(InvalidDescription2);
        Add(InvalidDescription3);
    }
}
