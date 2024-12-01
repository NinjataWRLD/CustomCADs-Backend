﻿using CustomCADs.Shared.Core;

namespace CustomCADs.Auth.Endpoints.Info.Authentication;

public class AuthenticationEndpoint
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("authentication");
        Group<InfoGroup>();
        Description(d => d
            .WithSummary("01. AuthN")
            .WithDescription("Are you logged in?")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(User.GetAuthentication()).ConfigureAwait(false);
    }
}
