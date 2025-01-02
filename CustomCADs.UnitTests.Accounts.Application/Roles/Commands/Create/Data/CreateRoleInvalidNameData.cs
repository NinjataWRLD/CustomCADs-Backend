namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;

using static RolesData;

public class CreateRoleInvalidNameData : CreateRoleData
{
    public CreateRoleInvalidNameData()
    {
        Add(InvalidName1, ValidDescription1);
        Add(InvalidName2, ValidDescription2);
        Add(InvalidName3, ValidDescription3);
    }
}
