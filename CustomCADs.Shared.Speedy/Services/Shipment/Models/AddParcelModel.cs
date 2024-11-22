using CustomCADs.Shared.Speedy.Services.Models;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Cod;

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record AddParcelModel(
    ShipmentParcelModel Parcel,
    double? CodAmount,
    ShipmentCodFiscalReceiptItemModel[] CodFiscalReceiptItems,
    double DeclaredValueAmount
);