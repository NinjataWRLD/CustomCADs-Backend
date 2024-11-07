using CustomCADs.Shared.Core.Events;
using CustomCADs.Shared.Core.Events.Email;
using Microsoft.Extensions.Configuration;

namespace CustomCADs.Auth.Endpoints.SignIn.ForgotPassword;

using static ApiMessages;
using static StatusCodes;

public class ForgotPasswordEndpoint(IUserService service, IEventRaiser raiser, IConfiguration config) : Endpoint<ForgotPasswordRequest>
{
    public override void Configure()
    {
        Get("forgotPassword");
        Group<SignInGroup>();
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByEmailAsync(req.Email).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("Email", UserNotFound, req.Email));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        string token = await service.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        string clientUrl = config["URLs:Client"] ?? "https://customcads.onrender.com";

        string endpoint = Path.Combine(clientUrl + "/login/reset-password") + $"?email={req.Email}&token={token}";

        PasswordResetRequestedEvent prrEvent = new(req.Email, endpoint);
        await raiser.PublishAsync(prrEvent).ConfigureAwait(false);

        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
