namespace CustomCADs.Identity.Endpoints.Identity.Post.ResetPassword;

public sealed record ResetPasswordRequest(string Email, string Token, string NewPassword);
