using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Wolverine;

namespace CustomCADs.Auth.Endpoints.Auth.ForgotPassword;

using static Helpers.ApiMessages;
using static StatusCodes;

public class ForgotPasswordEndpoint(IUserService service, IMessageBus bus, IConfiguration config) : Endpoint<ForgotPasswordRequest>
{
    public override void Configure()
    {
        Get("ForgotPassword");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByEmailAsync(req.Email).ConfigureAwait(false);
        if (user == null)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = string.Format(NotFound, "User"),
            });
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        string token = await service.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);
        string clientUrl = config["URLs:Client"] ?? "https://customcads.onrender.com";

        string endpoint = Path.Combine(clientUrl + "/login/reset-password") + $"?email={req.Email}&token={token}";

        EmailVerificationRequestedEvent @event = new(req.Email, endpoint);
        await bus.PublishAsync(@event).ConfigureAwait(false);

        await SendOkAsync("Check your email!").ConfigureAwait(false);
    }
}
