namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using static RolesData;

public class CreateRoleInvalidDescriptionData : CreateRoleData
{
    public CreateRoleInvalidDescriptionData()
    {
        Add(new(ValidName1, InvalidDescription1));
        Add(new(ValidName2, InvalidDescription2));
        Add(new(ValidName3, InvalidDescription3));
    }
}
