namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Delete.Data;

using static RolesData;

public class DeleteRoleHandlerValidData : DeleteRoleHandlerData
{
    public DeleteRoleHandlerValidData()
    {
        Add(ValidName1);
        Add(ValidName2);
        Add(ValidName3);
        Add(ValidName4);
    }
}
