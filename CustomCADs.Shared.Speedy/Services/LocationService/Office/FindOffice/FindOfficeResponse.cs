namespace CustomCADs.Shared.Speedy.Services.LocationService.Office.FindOffice;

using Dtos.Office;

public record FindOfficeResponse(
    OfficeDto[]? Offices,
    ErrorDto? Error
);