using CustomCADs.Speedy.Core.Models;

namespace CustomCADs.Speedy.Core.Contracts.Location;

public record FindNeaerestOfficeModel(
	ShipmentAddressModel Address,
	int? Distance,
	int? Limit,
	OfficeType? OfficeType,
	OfficeFeature[]? OfficeFeatures
);
