namespace CustomCADs.Identity.Application.Users.Dtos;

public record RefreshTokenDto(string Value, DateTimeOffset ExpiresAt);
