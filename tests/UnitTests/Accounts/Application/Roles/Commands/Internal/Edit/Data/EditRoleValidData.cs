namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit;
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
