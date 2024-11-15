namespace CustomCADs.Shared.Speedy.Services.ShipmentService.HandoverToMidwayCarrier;

using Dtos.ShipmentParcels;

public record HandoverToMidwayCarrierRequest(
    string UserName,
    string Password,
    ParcelHandoverDto[] Parcels,
    string? Language,
    long? ClientSystemId
);
