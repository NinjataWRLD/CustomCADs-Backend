using CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record BarcodeInformationModel(
    LabelInfoModel LabelInfo,
    PrimaryShipmentModel PrimaryShipment,
    string PrimaryParcelId,
    string ReturnShipmentId,
    string ReturnParcelId,
    string RedirectShipmentId,
    string RedirectParcelId,
    string InitialShipmentId,
    string InitialParcelId
);