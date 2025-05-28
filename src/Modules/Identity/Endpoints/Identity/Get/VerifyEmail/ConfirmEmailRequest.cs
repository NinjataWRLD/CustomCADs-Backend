namespace CustomCADs.Identity.Endpoints.Identity.Get.VerifyEmail;

public sealed record ConfirmEmailRequest(
	string Username,
	string Token
);
