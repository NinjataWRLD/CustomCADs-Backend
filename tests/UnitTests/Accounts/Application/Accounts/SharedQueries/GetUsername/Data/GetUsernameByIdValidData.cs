namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUsername.Data;

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
