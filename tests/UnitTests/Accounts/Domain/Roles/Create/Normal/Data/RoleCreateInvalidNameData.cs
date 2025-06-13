namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal.Data;

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
