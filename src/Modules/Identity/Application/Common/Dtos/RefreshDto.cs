namespace CustomCADs.Identity.Application.Common.Dtos;

public record RefreshDto(
    string Role,
    string Username,
    AccessTokenDto AccessToken,
    RefreshTokenDto? RefreshToken
);
