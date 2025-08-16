using CustomCADs.Speedy.Core.Services.Models;

namespace CustomCADs.Speedy.Core.Services.Location.Office;

public record FindNeaerestOfficeModel(
	ShipmentAddressModel Address,
	int? Distance,
	int? Limit,
	OfficeType? OfficeType,
	OfficeFeature[]? OfficeFeatures
);
