namespace CustomCADs.Shared.Speedy.API.Endpoints.CalculationEndpoints.Calculation;

using Dtos.CalculationResult;

public record CalculationResponse(
    CalculationResultDto[] Calculations,
    ErrorDto? Error
);