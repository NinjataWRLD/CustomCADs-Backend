using CustomCADs.Identity.Domain;

namespace CustomCADs.Identity.Endpoints.SignIn.RefreshToken;

using static AccountConstants;
using static ApiMessages;
using static StatusCodes;

public sealed class RefreshTokenEndpoint(IUserService userService, ITokenService tokenService)
    : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("refresh");
        Group<SignInGroup>();
        Description(d => d
            .WithSummary("02. Refresh")
            .WithDescription("Refresh your login")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string? rt = HttpContext.GetRefreshTokenCookie();
        if (string.IsNullOrEmpty(rt))
        {
            ValidationFailures.Add(new("RefreshToken", NoRefreshToken));
            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        AppUser? user = await userService.FindByRefreshTokenAsync(rt).ConfigureAwait(false);
        if (user is null)
        {
            ValidationFailures.Add(new("RefreshToken", UserNotFound, rt));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        if (user.RefreshTokenEndDate < DateTime.UtcNow)
        {
            HttpContext.DeleteAllCookies();
            ValidationFailures.Add(new("RefreshToken", RefreshTokenExpired, rt));

            await SendErrorsAsync(Status401Unauthorized).ConfigureAwait(false);
            return;
        }

        string username = user.UserName ?? string.Empty,
            role = await userService.GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto newJwt = tokenService.GenerateAccessToken(user.AccountId, username, role);
        HttpContext.SaveAccessTokenCookie(newJwt.Value, newJwt.EndDate);

        if (user.RefreshTokenEndDate >= DateTime.UtcNow.AddMinutes(1))
        {
            await SendOkAsync(NewRefreshTokenNotNeeded).ConfigureAwait(false);
            return;
        }

        string newRt = tokenService.GenerateRefreshToken();
        DateTime newRtEnd = DateTime.UtcNow.AddDays(RtDurationInDays);
        await userService.UpdateRefreshTokenAsync(user.Id, newRt, newRtEnd).ConfigureAwait(false);

        HttpContext.SaveRefreshTokenCookie(newRt, newRtEnd);
        HttpContext.SaveRoleCookie(role, newRtEnd);
        HttpContext.SaveUsernameCookie(username, newRtEnd);

        await SendOkAsync($"{NewRefreshTokenNotNeeded} {NewRefreshTokenGranted}").ConfigureAwait(false);
    }
}
