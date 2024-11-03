using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery(int Id) : IQuery<RoleReadDto>;