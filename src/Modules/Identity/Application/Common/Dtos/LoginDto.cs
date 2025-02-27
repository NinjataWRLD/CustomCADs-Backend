namespace CustomCADs.Identity.Application.Common.Dtos;

public record LoginDto(
    AccessTokenDto AccessToken,
    RefreshTokenDto RefreshToken,
    string Role
);
