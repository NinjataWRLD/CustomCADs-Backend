using CustomCADs.Auth.Application.Common.Dtos;
using CustomCADs.Auth.Domain;

namespace CustomCADs.Auth.Endpoints.SignIn.Login;

using static ApiMessages;
using static AuthConstants;
using static StatusCodes;

public sealed class LoginEndpoint(IUserService userService, ITokenService tokenService)
    : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post("login");
        Group<SignInGroup>();
        AllowAnonymous();
        Description(d => d
            .WithSummary("01. Login")
            .WithDescription("Log in to your account by providing your Username and Password and an optional Remember Me")
        );
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

        bool isPasswordValid = await userService.CheckPasswordAsync(user, req.Password).ConfigureAwait(false);
        if (!isPasswordValid)
        {
            ValidationFailures.Add(new("Password", InvalidLogin, req.Password));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        string role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.AccountId, req.Username, role);
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
