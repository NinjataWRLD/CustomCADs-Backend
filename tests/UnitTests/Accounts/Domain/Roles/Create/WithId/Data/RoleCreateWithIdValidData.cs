namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdValidData : RoleCreateWithIdData
{
    public RoleCreateWithIdValidData()
    {
        Add(ValidId, ValidName1, ValidDescription1);
        Add(ValidId, ValidName2, ValidDescription2);
        Add(ValidId, ValidName3, ValidDescription3);
        Add(ValidId, ValidName4, ValidDescription4);
    }
}
