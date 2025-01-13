namespace CustomCADs.Identity.Endpoints.SignUp.VerifyEmail;

public sealed record ConfirmEmailRequest(
    string Username,
    string? Token = default
);
