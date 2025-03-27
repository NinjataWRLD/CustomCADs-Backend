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
        AppUser user = await service.FindByEmailAsync(req.Email).ConfigureAwait(false);

        string encodedToken = req.Token.Replace(' ', '+');
        await service.ResetPasswordAsync(user, encodedToken, req.NewPassword).ConfigureAwait(false);

        await SendOkAsync("Done!").ConfigureAwait(false);
    }
}
