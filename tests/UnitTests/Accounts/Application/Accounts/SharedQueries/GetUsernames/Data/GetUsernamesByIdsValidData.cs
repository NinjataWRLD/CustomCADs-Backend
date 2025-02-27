namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsernames.Data;

using static AccountsData;

public class GetUsernamesByIdsValidData : GetUsernamesByIdsData
{
    public GetUsernamesByIdsValidData()
    {
        Add([ValidId1, ValidId2, ValidId3, ValidId4]);
    }
}
