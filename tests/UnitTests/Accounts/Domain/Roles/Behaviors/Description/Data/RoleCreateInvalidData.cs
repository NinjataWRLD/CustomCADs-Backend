namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description.Data;

using static RolesData;

public class RoleCreateInvalidData : RoleDescriptionData
{
	public RoleCreateInvalidData()
	{
		Add(InvalidDescription);
		Add(MinInvalidDescription);
		Add(MaxInvalidDescription);
	}
}
