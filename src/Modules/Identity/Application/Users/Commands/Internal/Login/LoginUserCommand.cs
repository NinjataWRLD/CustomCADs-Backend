using CustomCADs.Identity.Application.Users.Dtos;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Login;

public record LoginUserCommand(
    string Username,
    string Password,
    bool LongerExpireTime
) : ICommand<TokensDto>;
