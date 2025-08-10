namespace CustomCADs.Speedy.API.Dtos.ParcelToPrint;

public record LabelInfoDto(
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
