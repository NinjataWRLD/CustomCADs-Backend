namespace CustomCADs.Accounts.Endpoints.Users.Post;

public record PostAccountRequest(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string Password,
    string? FirstName = default,
    string? LastName = default
);
