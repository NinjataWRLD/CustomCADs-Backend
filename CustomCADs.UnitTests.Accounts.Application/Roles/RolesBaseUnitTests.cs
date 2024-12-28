namespace CustomCADs.UnitTests.Accounts.Application.Roles;

[TestFixture]
public class RolesBaseUnitTests
{
    protected const string Name = "Role Name";
    protected const string Description = "Role Description";
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Role CreateRole(string name = Name, string description = Description)
        => Role.Create(name, description);
}
