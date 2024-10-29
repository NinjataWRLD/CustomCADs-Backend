using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace CustomCADs.Auth.Endpoints.Auth.Authorization;

using static Helpers.ApiMessages;
using static StatusCodes;

public class AuthorizationEndpoint(IUserService serivce) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("Authorization");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        AppUser? user = await serivce.FindByIdAsync(User.GetId()).ConfigureAwait(false);
        if (user == null)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = NotFound,
                FormattedMessagePlaceholderValues = new() { ["0"] = "User" }
            });
            await SendErrorsAsync(Status401Unauthorized);
            return;
        }
        
        string role = await serivce.GetRoleAsync(user).ConfigureAwait(false);        
        await SendOkAsync(role).ConfigureAwait(false);
    }
}
