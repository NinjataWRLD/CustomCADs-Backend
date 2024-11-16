namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.GetStreet;

using Dtos.Street;

public record GetStreetResponse(
    StreetDto? Street,
    ErrorDto? Error
);