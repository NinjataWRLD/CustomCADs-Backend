namespace CustomCADs.UnitTests.Accounts.Application.Roles;

using static RolesData;

public class RolesBaseUnitTests
{
    public static readonly CancellationToken ct = CancellationToken.None;

    protected static Role CreateRole(string name = ValidName1, string description = ValidDescription2)
        => Role.Create(name, description);
}
