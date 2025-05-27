namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal.Data;

using CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal;
using static RolesData;

public class RoleCreateInvalidNameData : RoleCreateData
{
	public RoleCreateInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1);
		Add(InvalidName2, ValidDescription2);
		Add(InvalidName3, ValidDescription3);
	}
}
