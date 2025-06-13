namespace CustomCADs.UnitTests.Accounts.Domain.Roles;

using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using static RolesData;

public class RolesBaseUnitTests
{
	protected static Role CreateRole(
		string name = ValidName,
		string description = ValidDescription
	) => Role.Create(
			name: name,
			description: description
		);

	protected static Role CreateRoleWithId(
		RoleId? id = null,
		string name = ValidName,
		string description = ValidDescription
	) => Role.CreateWithId(
			id: id ?? ValidId,
			name: name,
			description: description
		);
}
