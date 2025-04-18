namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created.Data;

using CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created;
using static RolesData;

public class RoleCreatedValidData : RoleCreatedData
{
    public RoleCreatedValidData()
    {
        Add(Role.Create(ValidName1, ValidDescription1));
        Add(Role.Create(ValidName2, ValidDescription2));
        Add(Role.Create(ValidName3, ValidDescription3));
        Add(Role.Create(ValidName4, ValidDescription4));
    }
}
