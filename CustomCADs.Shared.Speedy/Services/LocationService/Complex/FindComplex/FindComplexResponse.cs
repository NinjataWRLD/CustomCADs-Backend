namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.FindComplex;

using Dtos.Complex;
using Dtos.Errors;

public record FindComplexResponse(
    ComplexDto[]? Complexes,
    ErrorDto? Error
);