namespace CustomCADs.Speedy.Core.Models;

public record AddressModel(
	string FullAddressString,
	string SiteAddressString,
	string LocalAddressString,
	ShipmentAddressModel ShipmentAddressModel
);
