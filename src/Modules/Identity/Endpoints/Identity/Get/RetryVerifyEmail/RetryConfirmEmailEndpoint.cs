﻿using Microsoft.AspNetCore.Routing;

namespace CustomCADs.Identity.Endpoints.Identity.Get.RetryVerifyEmail;

public sealed class RetryConfirmEmailEndpoint(IUserService service, LinkGenerator links)
    : Endpoint<RetryConfirmEmailRequest>
{
    public override void Configure()
    {
        Get("email/confirm/{username}/retry");
        Group<IdentityGroup>();
        Description(d => d
            .WithName(IdentityNames.RetryConfirmEmail)
            .WithSummary("Retry Send Email")
            .WithDescription("Receive another verification email")
        );
    }

    public override async Task HandleAsync(RetryConfirmEmailRequest req, CancellationToken ct)
    {
        AppUser user = await service.FindByNameAsync(req.Username).ConfigureAwait(false);

        string token = await service.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        string uri = links.GetUriByName(HttpContext, IdentityNames.ConfirmEmail, new { username = req.Username, token })
            ?? throw new InvalidOperationException("Unable to generate confirmation link.");

        await service.SendVerificationEmailAsync(user, uri).ConfigureAwait(false);

        await SendOkAsync("Check your email.").ConfigureAwait(false);
    }
}
