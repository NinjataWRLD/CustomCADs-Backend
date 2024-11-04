namespace CustomCADs.Auth.Endpoints.SignUp.VerifyEmail;

public record VerifyEmailRequest(string Username, string? Token = default);
