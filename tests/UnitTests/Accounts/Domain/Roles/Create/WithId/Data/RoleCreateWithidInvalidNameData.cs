namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidNameData : RoleCreateWithIdData
{
    public RoleCreateWithIdInvalidNameData()
    {
        Add(ValidId, InvalidName1, ValidDescription1);
        Add(ValidId, InvalidName2, ValidDescription2);
        Add(ValidId, InvalidName3, ValidDescription3);
    }
}
