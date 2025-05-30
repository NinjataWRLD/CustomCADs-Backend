namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidNameData : RoleCreateWithIdData
{
    public RoleCreateWithIdInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
