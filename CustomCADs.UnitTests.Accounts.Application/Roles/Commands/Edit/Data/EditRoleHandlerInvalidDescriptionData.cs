namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit.Data;

using static RolesData;

public class EditRoleHandlerInvalidDescriptionData : EditRoleHandlerData
{
    public EditRoleHandlerInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
