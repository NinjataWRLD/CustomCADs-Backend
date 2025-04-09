using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Tokens;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Login;

public class LoginUserHandler(IUserManager manager, ITokenService service)
    : ICommandHandler<LoginUserCommand, TokensDto>
{
    public async Task<TokensDto> Handle(LoginUserCommand req, CancellationToken ct)
    {
        User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw CustomNotFoundException<User>.ByProp(nameof(user.Username), req.Username);

        if (!user.Email.IsVerified)
        {
            throw CustomAuthorizationException<User>.Custom($"User: {user.Username} hasn't verified their email.");
        }

        DateTimeOffset? lockoutEnd = await manager.GetIsLockedOutAsync(user.Username).ConfigureAwait(false);
        if (lockoutEnd.HasValue)
        {
            TimeSpan timeLeft = lockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
            throw CustomAuthorizationException<User>.Custom($"The max attempts for logging into Account: {user.Username} has been reached. The account has been locked out for {seconds} seconds.");
        }

        if (!await manager.CheckPasswordAsync(user, req.Password).ConfigureAwait(false))
        {
            throw CustomAuthorizationException<User>.Custom($"Account: {user.Username} doesn't exist or password is incorrect.");
        }

        return await manager.IssueTokens(service, req.Username, req.LongerExpireTime).ConfigureAwait(false);
    }
}
