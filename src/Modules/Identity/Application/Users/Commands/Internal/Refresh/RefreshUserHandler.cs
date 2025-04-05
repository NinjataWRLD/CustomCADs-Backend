using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public class RefreshUserHandler(IUserManager manager, ITokenService service)
    : ICommandHandler<RefreshUserCommand, AccessTokenDto>
{
    public async Task<AccessTokenDto> Handle(RefreshUserCommand req, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(req.Token))
            throw CustomAuthorizationException<User>.Custom("No Refresh Token found.");

        User user = await manager.GetByRefreshTokenAsync(req.Token).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(User.RefreshToken), req.Token);

        if (user.RefreshToken?.ExpiresAt < DateTime.UtcNow)
            throw CustomAuthorizationException<User>.Custom("Refresh Token found, but expired.");

        return service.GenerateAccessToken(
            accountId: user.AccountId,
            username: user.Username,
            role: user.Role
        );
    }
}
