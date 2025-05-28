namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUserRole.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUserRole;
using static AccountsData;

public class GetUserRoleByIdValidData : GetUserRoleByIdData
{
	public GetUserRoleByIdValidData()
	{
		Add(ValidId1);
		Add(ValidId2);
		Add(ValidId3);
		Add(ValidId4);
	}
}
