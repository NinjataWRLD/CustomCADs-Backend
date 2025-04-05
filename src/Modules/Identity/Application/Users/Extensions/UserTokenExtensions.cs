using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Extensions;

using static Constants.Tokens;

internal static class UserTokenExtensions
{
    public static async Task<TokensDto> IssueTokens(this IUserManager manager, ITokenService tokens, string username, bool longerSession)
    {
        User user = await manager.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(user.Username), username);

        return new(
            Role: user.Role,
            AccessToken: tokens.GenerateAccessToken(
                accountId: user.AccountId,
                username: user.Username,
                role: user.Role
            ),
            RefreshToken: await manager.UpdateRefreshTokenAsync(
                id: user.Id,
                token: tokens.GenerateRefreshToken(),
                longerSession: longerSession
            ).ConfigureAwait(false)
        );

    }

    private static async Task<RefreshTokenDto> UpdateRefreshTokenAsync(this IUserManager manager, Guid id, string token, bool longerSession)
    {
        User user = await manager.GetByIdAsync(id).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ById(id);

        int days = longerSession ? LongerRtDurationInDays : RtDurationInDays;
        DateTimeOffset expiresAt = DateTimeOffset.UtcNow.AddDays(days);

        user.RefreshToken = new(token, expiresAt);
        await manager.UpdateAsync(user).ConfigureAwait(false);

        return new(token, expiresAt);
    }
}
