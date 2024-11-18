using CustomCADs.Account.Application.Users.Queries.GetAll;
using CustomCADs.Account.Application.Users.Queries.GetById;
using CustomCADs.Account.Application.Users.Queries.GetByUsername;

namespace CustomCADs.Account.Endpoints.Users;

public record UserResponse(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
