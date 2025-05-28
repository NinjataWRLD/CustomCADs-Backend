namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name.Data;

using static RolesData;

public class RoleNameInvalidData : RoleNameData
{
	public RoleNameInvalidData()
	{
		Add(InvalidName1);
		Add(InvalidName2);
		Add(InvalidName3);
	}
}
