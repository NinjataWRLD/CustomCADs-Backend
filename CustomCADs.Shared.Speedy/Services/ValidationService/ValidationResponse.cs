namespace CustomCADs.Shared.Speedy.Services.ValidationService;

using Dtos.Errors;

public record ValidationResponse(
    bool? Valid,
    ErrorDto? Error
);
