namespace CustomCADs.UnitTests.Accounts.Application.Accounts.SharedQueries.GetUserRole.Data;

using static AccountsData;

public class GetUserRoleByIdHandlerValidData : GetUserRoleByIdHandlerData
{
    public GetUserRoleByIdHandlerValidData()
    {
        Add(ValidId1);
        Add(ValidId2);
        Add(ValidId3);
        Add(ValidId4);
    }
}
