namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

public sealed record RegisterRequest(
    string Role,
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    string? FirstName = default,
    string? LastName = default
);
