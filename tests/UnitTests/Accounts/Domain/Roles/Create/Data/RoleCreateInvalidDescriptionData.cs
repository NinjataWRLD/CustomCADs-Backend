namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Data;

using static RolesData;

public class RoleCreateInvalidDescriptionData : RoleCreateData
{
	public RoleCreateInvalidDescriptionData()
	{
		Add(ValidName, InvalidDescription);
		Add(MinValidName, MinInvalidDescription);
		Add(MaxValidName, MaxInvalidDescription);
	}
}
