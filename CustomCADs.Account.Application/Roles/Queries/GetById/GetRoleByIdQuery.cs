using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery(RoleId Id) : IQuery<RoleReadDto>;