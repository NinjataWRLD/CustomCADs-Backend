namespace CustomCADs.Identity.Endpoints.Identity.Post.ResetPassword;

public sealed class ResetPasswordEndpoint(IUserService service)
    : Endpoint<ResetPasswordRequest>
{
    public override void Configure()
    {
        Post("password/reset");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.ResetPassword)
            .WithSummary("Reset Password")
            .WithDescription("Reset your Password with the token from the email")
        );
    }

    public override async Task HandleAsync(ResetPasswordRequest req, CancellationToken ct)
    {
        await service.ResetPasswordAsync(
            email: req.Email,
            token: req.Token.Replace(' ', '+'),
            newPassword: req.NewPassword
        ).ConfigureAwait(false);

        await SendOkAsync("Done!").ConfigureAwait(false);
    }
}
