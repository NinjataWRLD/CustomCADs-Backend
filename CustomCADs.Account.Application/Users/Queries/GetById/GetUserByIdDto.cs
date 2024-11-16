using CustomCADs.Account.Domain.Users.Entities;

namespace CustomCADs.Account.Application.Users.Queries.GetById;

public record GetUserByIdDto(
    string Role,
    string Username,
    string Email,
    string? FirstName,
    string? LastName
);
