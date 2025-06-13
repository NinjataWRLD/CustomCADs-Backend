namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description.Data;

using static RolesData;

public class RoleCreateValidData : RoleDescriptionData
{
	public RoleCreateValidData()
	{
		Add(ValidDescription);
		Add(MinValidDescription);
		Add(MaxValidDescription);
	}
}
