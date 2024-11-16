namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.GetStreet;

public record GetStreetRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);