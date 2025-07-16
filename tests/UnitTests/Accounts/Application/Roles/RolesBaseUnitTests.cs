namespace CustomCADs.UnitTests.Accounts.Application.Roles;

using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using static RolesData;

public class RolesBaseUnitTests
{
	public static readonly CancellationToken ct = CancellationToken.None;

	protected static Role CreateRole(string? name = null, string? description = null)
		=> Role.Create(name ?? ValidName, MinValidDescription);

	protected static Role CreateRoleWithId(RoleId? id = null, string? name = null, string? description = null)
		=> Role.CreateWithId(id ?? ValidId, name ?? ValidName, MinValidDescription);
}
