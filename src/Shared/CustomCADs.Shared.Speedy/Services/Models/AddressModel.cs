namespace CustomCADs.Shared.Speedy.Services.Models;

public record AddressModel(
    string FullAddressString,
    string SiteAddressString,
    string LocalAddressString,
    ShipmentAddressModel ShipmentAddressModel
);
