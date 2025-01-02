namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;

using static RolesData;

public class CreateRoleHandlerInvalidDescriptionData : CreateRoleHandlerData
{
    public CreateRoleHandlerInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
