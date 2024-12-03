namespace CustomCADs.Identity.Endpoints.SignIn.ResetPassword;

public sealed record ResetPasswordRequest(string Email, string Token, string NewPassword);
