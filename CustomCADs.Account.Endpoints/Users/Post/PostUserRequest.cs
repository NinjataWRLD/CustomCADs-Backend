namespace CustomCADs.Account.Endpoints.Users.Post;

public record PostUserRequest(
    string Role,
    string Username,
    string Email,
    string Password,
    string? FirstName = default,
    string? LastName = default
);
