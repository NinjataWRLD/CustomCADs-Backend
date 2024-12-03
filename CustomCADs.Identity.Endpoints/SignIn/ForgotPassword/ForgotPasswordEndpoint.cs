namespace CustomCADs.Identity.Endpoints.SignIn.ForgotPassword;

public sealed class ForgotPasswordEndpoint(IUserService service)
    : Endpoint<ForgotPasswordRequest>
{
    public override void Configure()
    {
        Get("forgotPassword");
        Group<SignInGroup>();
        Description(d => d
            .WithSummary("04. Reset Password Email")
            .WithDescription("Receive an Email with a link Reset your Password")
        );
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        await service.SendResetPasswordEmailAsync(req.Email).ConfigureAwait(false);

        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
