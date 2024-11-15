namespace CustomCADs.Shared.Speedy.Services.PrintService.LabelInfo;

using Dtos.ShipmentParcels;

public record LabelInfoRequest(
    string UserName,
    string Password,
    ShipmentParcelRefDto[] Parcels,
    string? Language,
    long? ClientSystemId
);
