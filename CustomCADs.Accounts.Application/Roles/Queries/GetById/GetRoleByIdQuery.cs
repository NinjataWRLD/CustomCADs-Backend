using CustomCADs.Accounts.Application.Roles.Queries;

namespace CustomCADs.Accounts.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery(RoleId Id) : IQuery<RoleReadDto>;