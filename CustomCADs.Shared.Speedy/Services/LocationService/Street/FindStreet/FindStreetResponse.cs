namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.FindStreet;

using Dtos.Street;

public record FindStreetResponse(
    StreetDto? Streets,
    ErrorDto? Error
);