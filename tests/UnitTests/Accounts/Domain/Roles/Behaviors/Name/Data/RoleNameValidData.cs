namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name.Data;

using static RolesData;

public class RoleNameValidData : RoleNameData
{
	public RoleNameValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
