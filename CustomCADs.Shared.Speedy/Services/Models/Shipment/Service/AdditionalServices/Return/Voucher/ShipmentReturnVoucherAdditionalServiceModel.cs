namespace CustomCADs.Shared.Speedy.Services.Models.Shipment.Service.AdditionalServices.Return.Voucher;

public record ShipmentReturnVoucherAdditionalServiceModel(
    int ServiceId,
    Payer Payer,
    int? ValidityPeriod
);