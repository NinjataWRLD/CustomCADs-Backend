namespace CustomCADs.Accounts.Endpoints.Accounts.Post;

public sealed record PostAccountRequest(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string Password,
    string? FirstName = default,
    string? LastName = default
);
