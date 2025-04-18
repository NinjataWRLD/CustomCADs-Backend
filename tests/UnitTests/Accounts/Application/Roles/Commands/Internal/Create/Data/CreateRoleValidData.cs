namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;
using static RolesData;

public class CreateRoleValidData : CreateRoleData
{
    public CreateRoleValidData()
    {
        Add(new(ValidName1, ValidDescription1));
        Add(new(ValidName2, ValidDescription2));
        Add(new(ValidName3, ValidDescription3));
        Add(new(ValidName4, ValidDescription4));
    }
}
