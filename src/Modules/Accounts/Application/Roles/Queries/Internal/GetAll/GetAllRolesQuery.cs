namespace CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;

public sealed record GetAllRolesQuery(
) : IQuery<IEnumerable<RoleReadDto>>;
