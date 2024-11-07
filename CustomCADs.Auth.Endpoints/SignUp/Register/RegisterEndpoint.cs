﻿using CustomCADs.Shared.Core.Events;
using CustomCADs.Shared.Core.Events.Email;
using CustomCADs.Shared.Core.Events.Users;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CustomCADs.Auth.Endpoints.SignUp.Register;

public class RegisterEndpoint(IUserService service, IEventRaiser raiser, IConfiguration config) 
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("register");
        Group<SignUpGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        AppUser user = new(req.Username, req.Email);
        IdentityResult result = await service.CreateAsync(user, req.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            ValidationFailures.AddRange(result.Errors
                .Select(e => new ValidationFailure(e.Code, e.Description)));

            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }
        await service.AddToRoleAsync(user, req.Role);

        UserRegisteredEvent urEvent = new(req.Role, req.Username, req.Email, req.FirstName, req.LastName);
        await raiser.PublishAsync(urEvent).ConfigureAwait(false);

        string token = await service.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        string serverUrl = config["URLs:Server"] ?? throw new ArgumentNullException();

        string endpoint = $"{serverUrl}/API/v1/Auth/VerifyEmail/{req.Username}?token={token}";

        EmailVerificationRequestedEvent evrEvent = new(user.Email ?? string.Empty, endpoint);
        await raiser.PublishAsync(evrEvent).ConfigureAwait(false);

        await SendOkAsync().ConfigureAwait(false);
    }
}
