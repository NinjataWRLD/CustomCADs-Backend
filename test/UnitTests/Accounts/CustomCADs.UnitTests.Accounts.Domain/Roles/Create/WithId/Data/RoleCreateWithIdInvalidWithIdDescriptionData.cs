namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidWithIdDescriptionData : RoleCreateWithIdData
{
    public RoleCreateWithIdInvalidWithIdDescriptionData()
    {
        Add(ValidId1, ValidName1, InvalidDescription1);
        Add(ValidId2, ValidName2, InvalidDescription2);
        Add(ValidId3, ValidName3, InvalidDescription3);
    }
}
