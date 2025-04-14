using CustomCADs.Identity.Application.Users.Dtos;
namespace CustomCADs.Identity.Application.Users.Commands.Internal.Refresh;

public record RefreshUserCommand(
    string? Token
) : ICommand<TokensDto>;
