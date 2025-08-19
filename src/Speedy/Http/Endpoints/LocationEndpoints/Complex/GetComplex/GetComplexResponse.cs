namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Complex.GetComplex;

using Dtos.Complex;

internal record GetComplexResponse(
	ComplexDto? Complex,
	ErrorDto? Error
);
