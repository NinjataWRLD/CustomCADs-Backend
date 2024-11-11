using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Endpoints.SignIn.Login;

using static ApiMessages;
using static AuthConstants;
using static StatusCodes;

public class LoginEndpoint(IUserService userService, ITokenService tokenService, ISignInService signInService)
    : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("login");
        Group<SignInGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        AppUser? user = await userService.FindByNameAsync(req.Username).ConfigureAwait(false);
        if (user is null || !user.EmailConfirmed)
        {
            ValidationFailures.Add(new(nameof(InvalidAccountOrEmail), InvalidAccountOrEmail));

            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        bool isLockedOut = await userService.IsLockedOutAsync(user).ConfigureAwait(false);
        if (isLockedOut && user.LockoutEnd.HasValue)
        {
            TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            await SendLockedOutAsync(timeLeft).ConfigureAwait(false);
            return;
        }

        SignInResult result = await signInService
            .PasswordSignInAsync(user, req.Password, req.RememberMe ?? false, lockoutOnFailure: true)
            .ConfigureAwait(false);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut && user.LockoutEnd.HasValue)
            {
                TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
                await SendLockedOutAsync(timeLeft).ConfigureAwait(false);
                return;
            }

            ValidationFailures.Add(new("Password", InvalidLogin, req.Password));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        UserId accountId = user.AccountId ?? throw UserAccountNotCreatedYetException.ByUsername(user.UserName ?? string.Empty);
        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);

        AccessTokenDto jwt = tokenService.GenerateAccessToken(accountId, req.Username, role);
        SaveJwt(jwt.Value, jwt.EndDate);

        string rt = tokenService.GenerateRefreshToken();
        DateTime rtEndDate = DateTime.UtcNow.AddDays(RtDurationInDays);

        await userService.UpdateRefreshTokenAsync(user.Id, rt, rtEndDate).ConfigureAwait(false);
        SaveRt(rt, rtEndDate);

        SaveRole(role, rtEndDate);
        SaveUsername(req.Username, rtEndDate);

        await SendOkAsync("Welcome back!").ConfigureAwait(false);
    }

    private async Task SendLockedOutAsync(TimeSpan timeLeft)
    {
        int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
        ValidationFailures.Add(new("Password", LockedOutUser)
        {
            FormattedMessagePlaceholderValues = new() { ["seconds"] = seconds },
            CustomState = seconds,
        });

        await SendErrorsAsync(Status423Locked).ConfigureAwait(false);
    }

    private void SaveJwt(string jwt, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("jwt", jwt, new()
        {
            HttpOnly = true,
            Secure = true,
            Expires = expire,
        });
    }

    private void SaveRt(string rt, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("rt", rt, new()
        {
            HttpOnly = true,
            Secure = true,
            Expires = expire,
        });
    }

    private void SaveRole(string role, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("role", role, new() { Expires = expire });
    }

    private void SaveUsername(string username, DateTime expire)
    {
        HttpContext.Response.Cookies.Append("username", username, new() { Expires = expire });
    }
}
