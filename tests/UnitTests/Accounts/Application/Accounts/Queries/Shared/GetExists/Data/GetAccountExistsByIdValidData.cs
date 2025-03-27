namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists.Data;

using CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetExists;
using static AccountsData;

public class GetAccountExistsByIdValidData : GetAccountExistsById
{
    public GetAccountExistsByIdValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
        Add(ValidId4);
    }
}
