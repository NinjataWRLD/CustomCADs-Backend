namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using static RolesData;

public class CreateRoleInvalidNameData : CreateRoleData
{
	public CreateRoleInvalidNameData()
	{
		Add(new(InvalidName, ValidDescription));
		Add(new(MinInvalidName, MinValidDescription));
		Add(new(MaxInvalidName, MaxValidDescription));
	}
}
