namespace CustomCADs.Auth.Endpoints.SignUp.Register;

public record RegisterRequest(
    string Role,
    string Username,
    string Email,
    string TimeZone,
    string Password,
    string ConfirmPassword,
    string? FirstName = default,
    string? LastName = default
);
