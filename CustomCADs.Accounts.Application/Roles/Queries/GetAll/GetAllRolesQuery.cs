using CustomCADs.Accounts.Application.Roles.Queries;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetAll;

public record GetAllRolesQuery(
    string? Name = null,
    string? Description = null,
    string Sorting = ""
) : IQuery<IEnumerable<RoleReadDto>>;
