namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Complex.FindComplex;

using Dtos.Complex;

internal record FindComplexResponse(
	ComplexDto[]? Complexes,
	ErrorDto? Error
);
