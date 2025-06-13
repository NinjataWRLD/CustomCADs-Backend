namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created.Data;

using static RolesData;

public class RoleCreatedValidData : RoleCreatedData
{
	public RoleCreatedValidData()
	{
		Add(Role.Create(ValidName, ValidDescription));
		Add(Role.Create(MinValidName, MinValidDescription));
		Add(Role.Create(MaxValidName, MaxValidDescription));
	}
}
