namespace CustomCADs.Identity.Endpoints.Identity.Post.Login;

public sealed record LoginRequest(
	string Username,
	string Password,
	bool? RememberMe = default
);
