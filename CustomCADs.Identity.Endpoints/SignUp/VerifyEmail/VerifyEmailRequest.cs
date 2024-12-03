namespace CustomCADs.Identity.Endpoints.SignUp.VerifyEmail;

public sealed record VerifyEmailRequest(
    string Username, 
    string? Token = default
);
