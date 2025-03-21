﻿using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.SignUp.Register;

public sealed class RegisterEndpoint(IUserService service, LinkGenerator links)
    : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("register");
        Group<SignUpGroup>();
        Description(d => d
            .WithName(SignUpNames.Register)
            .WithSummary("01. Register")
            .WithDescription("Register an Account")
        );
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        CreateUserDto dto = new(
            Role: req.Role,
            Username: req.Username,
            Email: req.Email,
            TimeZone: req.TimeZone,
            Password: req.Password,
            FirstName: req.FirstName,
            LastName: req.LastName
        );
        await service.CreateAsync(dto).ConfigureAwait(false);

        string token = await service.GenerateEmailConfirmationTokenAsync(req.Username).ConfigureAwait(false);
        string uri = links.GetUriByName(HttpContext, SignUpNames.ConfirmEmail, new { username = req.Username, token = token })
            ?? throw new InvalidOperationException("Unable to generate confirmation link.");

        await service.SendVerificationEmailAsync(req.Username, uri).ConfigureAwait(false);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }
}
