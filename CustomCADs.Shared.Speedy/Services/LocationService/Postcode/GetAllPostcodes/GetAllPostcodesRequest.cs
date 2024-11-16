namespace CustomCADs.Shared.Speedy.Services.LocationService.Postcode.GetAllPostcodes;

public record GetAllPostcodesRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);
