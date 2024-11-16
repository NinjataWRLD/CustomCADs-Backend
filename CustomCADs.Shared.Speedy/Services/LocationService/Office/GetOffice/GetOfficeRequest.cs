namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.GetOffice;

public record GetOfficeRequest(
    string UserName,
    string Password,
    string? Location,
    long? ClientSystemId
);