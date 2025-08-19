namespace CustomCADs.Speedy.Core.Contracts.Shipment;

public record LabelInfoModel(
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
