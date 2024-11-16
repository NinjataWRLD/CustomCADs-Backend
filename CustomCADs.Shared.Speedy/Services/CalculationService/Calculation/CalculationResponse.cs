namespace CustomCADs.Shared.Speedy.Services.CalculationService.Calculation;

using Dtos.CalculationResult;
using Dtos.Errors;

public record CalculationResponse(
    CalculationResultDto[] Calculations,
    ErrorDto? Error
);