namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName.Data;

using static RolesData;

public class GetRoleByNameHandlerValidData : GetRoleByNameHandlerData
{
    public GetRoleByNameHandlerValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
        Add(ValidName3);
        Add(ValidName4);
    }
}
