namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.GetComplex;

using Dtos.Complex;

public record GetComplexResponse(
    ComplexDto? Complex,
    ErrorDto? Error
);