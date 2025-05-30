namespace CustomCADs.UnitTests.Accounts.Domain.Roles;

using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
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

	protected static Role CreateRoleWithId(
		RoleId? id = null,
		string name = ValidName1,
		string description = ValidDescription1
	) => Role.CreateWithId(
			id: id ?? ValidId,
			name: name,
			description: description
		);
}
