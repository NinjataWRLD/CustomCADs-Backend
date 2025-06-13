namespace CustomCADs.UnitTests.Accounts.Application.Roles;

using static RolesData;

public class RolesBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Role CreateRole(string? name = null, string? description = null)
		=> Role.Create(name ?? ValidName, MinValidDescription);
}
