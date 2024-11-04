namespace CustomCADs.Auth.Endpoints.SignIn.ResetPassword;

public record ResetPasswordRequest(string Email, string Token, string NewPassword);
