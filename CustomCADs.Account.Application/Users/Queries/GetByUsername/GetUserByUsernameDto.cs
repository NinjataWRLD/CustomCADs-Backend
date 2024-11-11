using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;

namespace CustomCADs.Account.Application.Users.Queries.GetByUsername;

public record GetUserByUsernameDto(
    UserId Id,
    string Role,
    string Email,
    string? FirstName = null,
    string? LastName = null
);
