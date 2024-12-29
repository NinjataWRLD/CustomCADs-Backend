namespace CustomCADs.Accounts.Application.Roles.Queries.GetAll;

public sealed record GetAllRolesQuery(
) : IQuery<IEnumerable<RoleReadDto>>;
