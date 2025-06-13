namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Delete.Data;

using static RolesData;

public class DeleteRoleValidData : DeleteRoleData
{
	public DeleteRoleValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
