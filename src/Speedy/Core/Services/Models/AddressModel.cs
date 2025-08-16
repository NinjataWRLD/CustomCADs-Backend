namespace CustomCADs.Speedy.Core.Services.Models;

public record AddressModel(
	string FullAddressString,
	string SiteAddressString,
	string LocalAddressString,
	ShipmentAddressModel ShipmentAddressModel
);
