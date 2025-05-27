namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created;
using static RolesData;

public class RoleCreatedValidData : RoleCreatedData
{
	public RoleCreatedValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
		Add(ValidName4, ValidDescription4);
	}
}
