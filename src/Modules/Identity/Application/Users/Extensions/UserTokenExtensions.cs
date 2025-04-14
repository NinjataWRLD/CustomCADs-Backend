using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Extensions;

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
                token: tokens.GenerateRefreshToken(longerSession)
            ).ConfigureAwait(false),
            CsrfToken: tokens.GenerateCsrfToken()
        );

    }

    private static async Task<TokenDto> UpdateRefreshTokenAsync(this IUserManager manager, UserId id, TokenDto token)
    {
        User user = await manager.GetByIdAsync(id).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ById(id);

        user.SetRefreshToken(token.Value, token.ExpiresAt);
        await manager.UpdateAsync(user.Id, user).ConfigureAwait(false);

        return token;
    }
}
