namespace CustomCADs.Accounts.Application.Roles.Queries.GetById;

public sealed record GetRoleByIdQuery(
    RoleId Id
) : IQuery<RoleReadDto>;