namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId.Data;

using static RolesData;

public class RoleCreateWithIdValidData : RoleCreateWithIdData
{
	public RoleCreateWithIdValidData()
	{
		Add(ValidName1, ValidDescription1);
		Add(ValidName2, ValidDescription2);
		Add(ValidName3, ValidDescription3);
		Add(ValidName4, ValidDescription4);
	}
}
