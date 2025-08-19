namespace CustomCADs.Speedy.Http.Endpoints.CalculationEndpoints.Calculation;

using Dtos.CalculationResult;

internal record CalculationResponse(
	CalculationResultDto[] Calculations,
	ErrorDto? Error
);
