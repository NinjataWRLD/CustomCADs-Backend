namespace CustomCADs.UnitTests.Accounts.Domain.Roles;

using CustomCADs.Shared.Domain.TypedIds.Accounts;
using static RolesData;

public class RolesBaseUnitTests
{
	protected static Role CreateRole(
		string? name = null,
		string description = ValidDescription
	) => Role.Create(
			name: name ?? ValidName,
			description: description
		);

	protected static Role CreateRoleWithId(
		RoleId? id = null,
		string? name = null,
		string description = ValidDescription
	) => Role.CreateWithId(
			id: id ?? ValidId,
			name: name ?? ValidName,
			description: description
		);
}
