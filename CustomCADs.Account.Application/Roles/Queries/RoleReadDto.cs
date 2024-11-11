using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Roles.Queries;

public record RoleReadDto(RoleId Id, string Name, string Description);