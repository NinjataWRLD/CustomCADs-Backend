namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidWithIdDescriptionData : RoleCreateWithIdData
{
    public RoleCreateWithIdInvalidWithIdDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
