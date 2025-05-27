namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Delete.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Delete;
using static RolesData;

public class DeleteRoleValidData : DeleteRoleData
{
	public DeleteRoleValidData()
	{
		Add(ValidName1);
		Add(ValidName2);
		Add(ValidName3);
		Add(ValidName4);
	}
}
