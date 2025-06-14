namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using static RolesData;

public class CreateRoleInvalidDescriptionData : CreateRoleData
{
	public CreateRoleInvalidDescriptionData()
	{
		Add(new(ValidName, InvalidDescription));
		Add(new(MinValidName, MinInvalidDescription));
		Add(new(MaxValidName, MaxInvalidDescription));
	}
}
