namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit.Data;

using static RolesData;

public class EditRoleValidData : EditRoleData
{
    public EditRoleValidData()
    {
        Add(ValidName1, ValidDescription1);
        Add(ValidName2, ValidDescription2);
        Add(ValidName3, ValidDescription3);
        Add(ValidName4, ValidDescription4);
    }
}
