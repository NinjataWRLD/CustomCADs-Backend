namespace CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Complex.GetComplex;

using Dtos.Complex;

public record GetComplexResponse(
	ComplexDto? Complex,
	ErrorDto? Error
);
