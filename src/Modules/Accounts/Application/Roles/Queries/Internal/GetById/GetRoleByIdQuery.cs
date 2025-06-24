namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;

public sealed record GetRoleByIdQuery(
	RoleId Id
) : IQuery<RoleReadDto>;
