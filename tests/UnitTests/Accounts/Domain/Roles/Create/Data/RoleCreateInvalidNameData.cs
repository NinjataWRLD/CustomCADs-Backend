namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Data;

using static RolesData;

public class RoleCreateInvalidNameData : RoleCreateData
{
	public RoleCreateInvalidNameData()
	{
		Add(InvalidName, ValidDescription);
		Add(MinInvalidName, MinValidDescription);
		Add(MaxInvalidName, MaxValidDescription);
	}
}
