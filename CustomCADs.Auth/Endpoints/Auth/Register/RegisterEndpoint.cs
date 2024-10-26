﻿using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Auth.Data.Entities;
using CustomCADs.Shared.Application.Email;
using CustomCADs.Shared.Events.Events;
using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Wolverine;

namespace CustomCADs.Auth.Endpoints.Auth.Register;

public class RegisterEndpoint(
    IMessageBus bus,
    IUserManager manager,
    IEmailService emailService,
    IConfiguration config) : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("Register/{role}");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        AppUser user = new(req.Username, req.Email);
        IdentityResult result = await manager.CreateAsync(user, req.Password).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            ValidationFailures.AddRange(result.Errors
                .Select(e => new ValidationFailure(e.Code, e.Description)));

            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }
        await manager.AddToRoleAsync(user, req.Role);

        UserRegisteredEvent @event = new()
        {
            Role = req.Role,
            Username = req.Username,
            Email = req.Email,
            FirstName = req.FirstName,
            LastName = req.LastName,
        };
        await bus.PublishAsync(@event).ConfigureAwait(false);

        string token = await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        string serverUrl = config["URLs:Server"] ?? throw new ArgumentNullException();

        string endpoint = Path.Combine(serverUrl, $"API/v1/Auth/VerifyEmail/{req.Username}?token={token}");
        await emailService.SendVerificationEmailAsync(req.Email, endpoint);

        await SendOkAsync().ConfigureAwait(false);
    }
}
