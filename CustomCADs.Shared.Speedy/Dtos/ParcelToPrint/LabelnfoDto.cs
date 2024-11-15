namespace CustomCADs.Shared.Speedy.Dtos.ParcelToPrint;

public record LabelnfoDto(
    string ParcelId,
    string FullBarcode,
    int ExportPriority,
    int? HubId,
    int? OfficeId,
    string? OfficeName,
    int? DeadlineDay,
    int? DeadlineMonth,
    int? TourId
);
