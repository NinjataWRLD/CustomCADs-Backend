using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.Identity.Application.Users.Dtos;

public record TokensDto(
    AccessTokenDto AccessToken,
    RefreshTokenDto RefreshToken,
    string Role
);
