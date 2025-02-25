namespace CustomCADs.Accounts.Application.Roles.Queries.GetByName;

public sealed record GetRoleByNameQuery(
    string Name
) : IQuery<RoleReadDto>;