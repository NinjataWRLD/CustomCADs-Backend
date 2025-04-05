using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPassword;

namespace CustomCADs.Identity.Endpoints.Identity.Post.ResetPassword;

public sealed class ResetPasswordEndpoint(IRequestSender sender)
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
        await sender.SendCommandAsync(command: new ResetUserPasswordCommand(
            Email: req.Email,
            Token: req.Token.Replace(' ', '+'),
            NewPassword: req.NewPassword
        ), ct).ConfigureAwait(false);

        await SendOkAsync("Done!").ConfigureAwait(false);
    }
}
