namespace CustomCADs.Identity.Application.Common.Dtos;

public record TokensDto(
    AccessTokenDto AccessToken,
    RefreshTokenDto RefreshToken,
    string Role
);
