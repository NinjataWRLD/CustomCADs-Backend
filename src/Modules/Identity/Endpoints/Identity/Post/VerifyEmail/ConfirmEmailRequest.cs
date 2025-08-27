namespace CustomCADs.Identity.Endpoints.Identity.Post.VerifyEmail;

public sealed record ConfirmEmailRequest(
	string Username,
	string Token
);
