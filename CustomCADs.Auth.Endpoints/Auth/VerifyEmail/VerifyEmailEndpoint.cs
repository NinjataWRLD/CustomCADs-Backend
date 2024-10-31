using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Infrastructure.Entities;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using static CustomCADs.Auth.Infrastructure.AuthConstants;

namespace CustomCADs.Auth.Endpoints.Auth.VerifyEmail;

using static Helpers.ApiMessages;
using static StatusCodes;

public class VerifyEmailEndpoint(IUserService userService, ITokenService tokenService) : Endpoint<VerifyEmailRequest>
{
    public override void Configure()
    {
        Get("VerifyEmail/{username}");
        Group<AuthGroup>();
    }

    public override async Task HandleAsync(VerifyEmailRequest req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(req.Token))
        {
            ValidationFailures.Add(new("EmailToken", IsRequired, req.Token)
            {
                FormattedMessagePlaceholderValues = new() { ["0"] = "Email Token" },
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        AppUser? user = await userService.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user == null)
        {
            ValidationFailures.Add(new("Username", UserNotFound, req.Username));
            await SendErrorsAsync(Status404NotFound).ConfigureAwait(false);
            return;
        }

        if (user.EmailConfirmed)
        {
            ValidationFailures.Add(new("Email", EmailAlreadyVerified));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        string decodedEct = req.Token.Replace(' ', '+');
        IdentityResult result = await userService.ConfirmEmailAsync(user, decodedEct).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            ValidationFailures.Add(new("EmailToken", InvalidEmailToken, decodedEct));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.Id, req.Username, role);
        SaveAccessToken(jwt.Value, jwt.EndDate);
        
        string rt = tokenService.GenerateRefreshToken();
        DateTime end = DateTime.UtcNow.AddDays(RtDurationInDays);
        SaveRefreshToken(rt, end);
        
        SaveRole(role, end);
        SaveUsername(req.Username, end);

        await SendOkAsync("Welcome!").ConfigureAwait(false);
    }

    private void SaveAccessToken(string jwt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("jwt", jwt, new() { HttpOnly = true, Secure = true, Expires = end });
    }
    
    private void SaveRefreshToken(string rt, DateTime end)
    {
        HttpContext.Response.Cookies.Append("rt", rt, new() { HttpOnly = true, Secure = true, Expires = end });
    }
    
    private void SaveRole(string role, DateTime end)
    {
        HttpContext.Response.Cookies.Append("role", role, new() { Expires = end });
    }

    private void SaveUsername(string username, DateTime end)
    {
        HttpContext.Response.Cookies.Append("username", username, new() { Expires = end });
    }
}
