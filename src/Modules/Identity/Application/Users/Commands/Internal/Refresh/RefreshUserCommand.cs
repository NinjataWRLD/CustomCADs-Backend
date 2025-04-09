using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public record RefreshUserCommand(
    string? Token
) : ICommand<AccessTokenDto>;
