namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;

using static RolesData;

public class CreateRoleInvalidDescriptionData : CreateRoleData
{
    public CreateRoleInvalidDescriptionData()
    {
        Add(ValidName1, InvalidDescription1);
        Add(ValidName2, InvalidDescription2);
        Add(ValidName3, InvalidDescription3);
    }
}
