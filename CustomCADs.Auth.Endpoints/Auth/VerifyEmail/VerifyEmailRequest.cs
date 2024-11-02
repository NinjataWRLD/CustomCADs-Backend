namespace CustomCADs.Auth.Endpoints.Auth.VerifyEmail;

public record VerifyEmailRequest(string Username, string? Token = default);
