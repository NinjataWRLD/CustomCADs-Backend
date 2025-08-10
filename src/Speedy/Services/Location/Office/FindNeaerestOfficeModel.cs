using CustomCADs.Speedy.API.Endpoints.LocationEndpoints.Enums;
using CustomCADs.Speedy.Services.Models;

namespace CustomCADs.Speedy.Services.Location.Office;

public record FindNeaerestOfficeModel(
	ShipmentAddressModel Address,
	int? Distance,
	int? Limit,
	OfficeType? OfficeType,
	OfficeFeature[]? OfficeFeatures
);
