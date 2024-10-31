using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Wolverine;

namespace CustomCADs.Auth.Endpoints.Auth.RetryVerifyEmail;

using static Helpers.ApiMessages;
using static StatusCodes;

public class RetryVerifyEmailEndpoint(IUserService service, IMessageBus bus, IConfiguration config) : Endpoint<RetryVerifyEmailRequest>
{
    public override void Configure()
    {
        Get("RetryVerifyEmail/{username}");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(RetryVerifyEmailRequest req, CancellationToken ct)
    {
        AppUser? user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user == null)
        {
            ValidationFailures.Add(new("Name", UserNotFound, req.Username));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        if (user.EmailConfirmed)
        {
            ValidationFailures.Add(new("Email", EmailAlreadyVerified));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        string serverUrl = config["URLs:Server"] ?? throw new ArgumentNullException("No Server Url");
        string token = await service.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

        string endpoint = Path.Combine(serverUrl, $"API/Identity/VerifyEmail/{req.Username}?token={token}");

        EmailVerificationRequestedEvent @event = new(user.Email ?? string.Empty, endpoint);
        await bus.PublishAsync(@event).ConfigureAwait(false);

        await SendOkAsync("Check your email.").ConfigureAwait(false);
    }
}
