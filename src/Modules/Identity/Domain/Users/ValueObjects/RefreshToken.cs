namespace CustomCADs.Identity.Domain.Users.ValueObjects;

public record RefreshToken(
    string Value,
    DateTimeOffset ExpiresAt
);
