namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.FindStreet;

using Dtos.Errors;
using Dtos.Street;

public record FindStreetResponse(
    StreetDto? Streets,
    ErrorDto? Error
);