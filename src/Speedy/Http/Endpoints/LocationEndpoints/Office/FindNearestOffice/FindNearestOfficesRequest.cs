namespace CustomCADs.Speedy.Http.Endpoints.LocationEndpoints.Office.FindNearestOffice;

using Dtos.ShipmentSenderAndRecipient.ShipmentAddress;
using Enums;

internal record FindNearestOfficesRequest(
	string UserName,
	string Password,
	ShipmentAddressDto Address,
	string? Language,
	long? ClientSystemId,
	int? Distance,
	int? Limit,
	OfficeType? OfficeType,
	OfficeFeature[]? OfficeFeatures
);
