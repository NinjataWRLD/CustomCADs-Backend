namespace CustomCADs.Shared.Speedy.Services.CalculationService.Calculation;

using Dtos.CalculationResult;

public record CalculationResponse(
    CalculationResultDto[] Calculations,
    ErrorDto? Error
);