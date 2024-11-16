namespace CustomCADs.Shared.Speedy.Services.ValidationService;

public record ValidationResponse(
    bool? Valid,
    ErrorDto? Error
);
