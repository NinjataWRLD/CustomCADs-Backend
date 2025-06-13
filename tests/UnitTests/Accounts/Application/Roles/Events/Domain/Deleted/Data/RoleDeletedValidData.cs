namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted.Data;

using static RolesData;

public class RoleDeletedValidData : RoleDeletedData
{
	public RoleDeletedValidData()
	{
		Add(Role.Create(ValidName, ValidDescription));
		Add(Role.Create(MinValidName, MinValidDescription));
		Add(Role.Create(MaxValidName, MaxValidDescription));
	}
}
