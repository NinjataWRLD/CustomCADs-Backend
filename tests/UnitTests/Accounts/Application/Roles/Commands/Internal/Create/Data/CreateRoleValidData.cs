namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;
using static RolesData;

public class CreateRoleValidData : CreateRoleData
{
	public CreateRoleValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
		Add(ValidName4, ValidDescription4);
	}
}
