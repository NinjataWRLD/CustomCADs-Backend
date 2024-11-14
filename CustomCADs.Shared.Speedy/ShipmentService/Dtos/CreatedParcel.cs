namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record CreatedParcel(
    int SeqNo,
    string Id,
    int? ExternalCarrierId,
    string? ExternalCarrierParcelNumber
);
