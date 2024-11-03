using CustomCADs.Account.Application.Common.Contracts;

namespace CustomCADs.Account.Application.Roles.Queries.GetByName;

public record GetRoleByNameQuery(string Name) : IQuery<RoleReadDto>;