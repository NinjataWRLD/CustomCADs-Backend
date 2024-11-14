namespace CustomCADs.Shared.Speedy.ShipmentService.Dtos;

public record Content(
    string Contents,
    string Package,
    ShipmentParcel[] Parcels
);
