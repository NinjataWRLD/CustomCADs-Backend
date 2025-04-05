using CustomCADs.Identity.Application.Users.Dtos;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.VerifyEmail;

public record class VerifyUserEmailCommand(
    string Username,
    string Token
) : ICommand<TokensDto>;
