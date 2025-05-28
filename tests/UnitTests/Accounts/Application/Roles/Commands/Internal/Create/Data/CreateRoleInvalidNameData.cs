namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;
using static RolesData;

public class CreateRoleInvalidNameData : CreateRoleData
{
	public CreateRoleInvalidNameData()
	{
		Add(InvalidName1, ValidDescription1);
		Add(InvalidName2, ValidDescription2);
		Add(InvalidName3, ValidDescription3);
	}
}
