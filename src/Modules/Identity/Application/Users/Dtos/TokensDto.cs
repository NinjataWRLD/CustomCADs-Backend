using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Application.Users.Dtos;

public record TokensDto(
    TokenDto AccessToken,
    TokenDto RefreshToken,
    string Role
);
