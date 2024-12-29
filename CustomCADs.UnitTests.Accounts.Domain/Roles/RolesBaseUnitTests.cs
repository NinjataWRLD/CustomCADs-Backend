namespace CustomCADs.UnitTests.Accounts.Domain.Roles;

using static RolesData;

public class RolesBaseUnitTests
{
    protected static Role CreateRole(
        string name = ValidName1,
        string description = ValidDescription1
    ) => Role.Create(
            name: name,
            description: description
        );
}
