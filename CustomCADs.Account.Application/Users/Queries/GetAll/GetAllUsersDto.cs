using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Account.Application.Users.Queries.GetAll;

public record GetAllUsersItem(
    UserId Id,
    string Username,
    string Email,
    string Role,
    string? FirstName = null,
    string? LastName = null
);
