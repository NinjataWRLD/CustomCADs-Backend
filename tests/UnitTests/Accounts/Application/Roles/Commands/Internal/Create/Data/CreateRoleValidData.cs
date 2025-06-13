namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using static RolesData;

public class CreateRoleValidData : CreateRoleData
{
	public CreateRoleValidData()
	{
		Add(new(ValidName, ValidDescription));
		Add(new(MinValidName, MinValidDescription));
		Add(new(MaxValidName, MaxValidDescription));
	}
}
