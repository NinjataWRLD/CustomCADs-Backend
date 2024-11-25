using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery(RoleId Id) : IQuery<RoleReadDto>;