namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.GetComplex;

using CustomCADs.Shared.Speedy.Dtos.Errors;
using Dtos.Complex;

public record GetComplexResponse(
    ComplexDto? Complex,
    ErrorDto? Error
);