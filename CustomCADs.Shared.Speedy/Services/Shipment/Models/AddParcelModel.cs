using CustomCADs.Shared.Speedy.Models;
using CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Cod;

namespace CustomCADs.Shared.Speedy.Services.Shipment.Models;

public record AddParcelModel(
    ShipmentParcelModel Parcel,
    double? CodAmount,
    ShipmentCodFiscalReceiptItemModel[] CodFiscalReceiptItems,
    double DeclaredValueAmount
);