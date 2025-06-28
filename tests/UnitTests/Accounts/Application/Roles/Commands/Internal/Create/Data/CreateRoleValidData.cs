namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using static RolesData;

public class CreateRoleValidData : TheoryData<RoleWriteDto>
{
	public CreateRoleValidData()
	{
		Add(new(ValidName, ValidDescription));
		Add(new(MinValidName, MinValidDescription));
		Add(new(MaxValidName, MaxValidDescription));
	}
}
