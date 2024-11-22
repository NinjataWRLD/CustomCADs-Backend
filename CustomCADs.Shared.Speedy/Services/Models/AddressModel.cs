using CustomCADs.Shared.Speedy.Services.Models;

namespace CustomCADs.Shared.Speedy.Models;

public record AddressModel(
    string FullAddressString,
    string SiteAddressString,
    string LocalAddressString,
    ShipmentAddressModel ShipmentAddressModel
);
