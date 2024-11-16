namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.GetStreet;

using Dtos.Errors;
using Dtos.Street;

public record GetStreetResponse(
    StreetDto? Street,
    ErrorDto? Error
);