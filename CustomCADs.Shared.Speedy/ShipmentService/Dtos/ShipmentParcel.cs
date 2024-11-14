namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record ShipmentParcel(
    ShipmentParccelSize Size,
    double Weight,
    string? Id,
    int? SeqNo
);
