namespace CustomCADs.Shared.Speedy.Models.Shipment.Service.AdditionalServices.Return.Voucher;

public record ShipmentReturnVoucherAdditionalServiceModel(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);