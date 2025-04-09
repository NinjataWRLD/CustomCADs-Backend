namespace CustomCADs.Shared.Abstractions.Tokens;

public record AccessTokenDto(string Value, DateTimeOffset ExpiresAt);