using CustomCADs.Shared.Speedy.API.Endpoints.LocationEndpoints.Enums;
using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Services.Location.Office;

public record FindNeaerestOfficeModel(
	ShipmentAddressModel Address,
	int? Distance,
	int? Limit,
	OfficeType? OfficeType,
	OfficeFeature[]? OfficeFeatures
);
