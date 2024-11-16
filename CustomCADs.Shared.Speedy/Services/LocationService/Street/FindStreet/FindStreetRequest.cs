namespace CustomCADs.Shared.Speedy.Services.LocationService.Street.FindStreet;

public record FindStreetRequest(
    string UserName,
    string Password,
    int SiteId,
    string? Location,
    long? ClientSystemId,
    string? Name,
    string? Type
);