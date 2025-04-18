namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;
using static RolesData;

public class CreateRoleInvalidNameData : CreateRoleData
{
    public CreateRoleInvalidNameData()
    {
        Add(new(InvalidName1, ValidDescription1));
        Add(new(InvalidName2, ValidDescription2));
        Add(new(InvalidName3, ValidDescription3));
    }
}
