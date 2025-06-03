namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetByName;

public sealed record GetRoleByNameQuery(
	string Name
) : IQuery<RoleReadDto>;
