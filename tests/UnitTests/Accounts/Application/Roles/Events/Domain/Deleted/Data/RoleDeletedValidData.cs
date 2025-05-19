namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted;
using static RolesData;

public class RoleDeletedValidData : RoleDeletedData
{
	public RoleDeletedValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
		Add(ValidName4, ValidDescription4);
	}
}
