namespace CustomCADs.Accounts.Application.Roles.Queries.GetAll;

public sealed record GetAllRolesQuery(
    string? Name = null,
    string? Description = null,
    string Sorting = ""
) : IQuery<IEnumerable<RoleReadDto>>;
