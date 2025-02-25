namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdValidData : RoleCreateWithIdData
{
    public RoleCreateWithIdValidData()
    {
        Add(ValidId1, ValidName1, ValidDescription1);
        Add(ValidId2, ValidName2, ValidDescription2);
        Add(ValidId3, ValidName3, ValidDescription3);
        Add(ValidId4, ValidName4, ValidDescription4);
    }
}
