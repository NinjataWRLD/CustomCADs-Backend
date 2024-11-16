namespace CustomCADs.Shared.Speedy.Services.LocationService.Complex.FindComplex;

using Dtos.Complex;

public record FindComplexResponse(
    ComplexDto[]? Complexes,
    ErrorDto? Error
);