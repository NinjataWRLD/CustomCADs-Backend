namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdInvalidNameData : RoleCreateWithIdData
{
	public RoleCreateWithIdInvalidNameData()
	{
		Add(ValidId1, InvalidName1, ValidDescription1);
		Add(ValidId2, InvalidName2, ValidDescription2);
		Add(ValidId3, InvalidName3, ValidDescription3);
	}
}
