namespace CustomCADs.Shared.Application.Abstractions.Cache;

public record Expiration(TimeSpan? Absolute, TimeSpan? Sliding);
