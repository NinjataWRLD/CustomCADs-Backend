using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Domain;

namespace CustomCADs.Identity.Endpoints.SignIn.Login;

using static AccountConstants;
using static ApiMessages;
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

        string rt = tokenService.GenerateRefreshToken();
        DateTime rtEndDate = DateTime.UtcNow.AddDays(RtDurationInDays);
        await userService.UpdateRefreshTokenAsync(user.Id, rt, rtEndDate).ConfigureAwait(false);

        HttpContext.SaveAccessTokenCookie(jwt.Value, jwt.EndDate);
        HttpContext.SaveRefreshTokenCookie(rt, rtEndDate);
        HttpContext.SaveRoleCookie(role, rtEndDate);
        HttpContext.SaveUsernameCookie(req.Username, rtEndDate);

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
}
