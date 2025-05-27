namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsernames.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsernames;
using static AccountsData;

public class GetUsernamesByIdsValidData : GetUsernamesByIdsData
{
	public GetUsernamesByIdsValidData()
	{
		Add([ValidId1, ValidId2, ValidId3, ValidId4]);
	}
}
