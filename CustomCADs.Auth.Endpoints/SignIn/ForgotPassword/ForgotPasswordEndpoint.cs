using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Core.Events;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Wolverine;

namespace CustomCADs.Auth.Endpoints.SignIn.ForgotPassword;

using static Helpers.ApiMessages;
using static StatusCodes;

public class ForgotPasswordEndpoint(IUserService service, IMessageBus bus, IConfiguration config) : Endpoint<ForgotPasswordRequest>
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
        await bus.PublishAsync(prrEvent).ConfigureAwait(false);

        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
