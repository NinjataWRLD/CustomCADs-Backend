namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsername.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsername;
using static AccountsData;

public class GetUsernameByIdValidData : GetUsernameByIdData
{
	public GetUsernameByIdValidData()
	{
		Add(ValidId1);
		Add(ValidId2);
		Add(ValidId3);
		Add(ValidId4);
	}
}
