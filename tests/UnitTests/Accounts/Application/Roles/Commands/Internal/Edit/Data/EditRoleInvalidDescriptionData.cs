namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit;
using static RolesData;

public class EditRoleInvalidDescriptionData : EditRoleData
{
    public EditRoleInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
