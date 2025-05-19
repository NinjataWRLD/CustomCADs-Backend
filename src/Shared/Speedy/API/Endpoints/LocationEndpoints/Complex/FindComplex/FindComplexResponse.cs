namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Complex.FindComplex;

using Dtos.Complex;

public record FindComplexResponse(
	ComplexDto[]? Complexes,
	ErrorDto? Error
);
