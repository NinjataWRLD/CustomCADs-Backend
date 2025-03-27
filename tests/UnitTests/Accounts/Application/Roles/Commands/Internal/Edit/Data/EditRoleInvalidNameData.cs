namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit;
using static RolesData;

public class EditRoleInvalidNameData : EditRoleData
{
    public EditRoleInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
