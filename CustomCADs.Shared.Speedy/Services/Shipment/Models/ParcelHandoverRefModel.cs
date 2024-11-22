using CustomCADs.Shared.Speedy.Services.Models.Shipment.Parcel;

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record ParcelHandoverRefModel(
    DateTime DateTime,
    ShipmentParcelRefModel Parcel
);
