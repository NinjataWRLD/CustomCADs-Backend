namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal.Data;

using CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal;
using static RolesData;

public class RoleCreateInvalidDescriptionData : RoleCreateData
{
	public RoleCreateInvalidDescriptionData()
	{
		Add(ValidName1, InvalidDescription1);
		Add(ValidName2, InvalidDescription2);
		Add(ValidName3, InvalidDescription3);
	}
}
