namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal.Data;

using static RolesData;

public class RoleCreateValidData : RoleCreateData
{
	public RoleCreateValidData()
	{
		Add(ValidName, ValidDescription);
		Add(MinValidName, MinValidDescription);
		Add(MaxValidName, MaxValidDescription);
	}
}
