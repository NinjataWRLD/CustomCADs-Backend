namespace CustomCADs.Shared.Speedy;

public record Error(
    string Message,
    string Id,
    int Code,
    string? Context,
    string? Component
);
