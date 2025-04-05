using CustomCADs.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

namespace CustomCADs.Identity.Endpoints.Identity.Get.ForgotPassword;

public sealed class ForgotPasswordEndpoint(IRequestSender sender)
    : Endpoint<ForgotPasswordRequest>
{
    public override void Configure()
    {
        Get("password/forgot/{email}");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.ForgotPassword)
            .WithSummary("Reset Password Email")
            .WithDescription("Receive an Email with a link to reset your Password")
        );
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(command: new ResetPasswordEmailCommand(
            Email: req.Email
        ), ct).ConfigureAwait(false);

        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
