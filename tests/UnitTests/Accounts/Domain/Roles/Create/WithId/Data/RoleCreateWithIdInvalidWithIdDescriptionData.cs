namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidWithIdDescriptionData : RoleCreateWithIdData
{
    public RoleCreateWithIdInvalidWithIdDescriptionData()
    {
        Add(ValidId, ValidName1, InvalidDescription1);
        Add(ValidId, ValidName2, InvalidDescription2);
        Add(ValidId, ValidName3, InvalidDescription3);
    }
}
