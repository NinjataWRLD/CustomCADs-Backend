using CustomCADs.Shared.Speedy.API.Dtos.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Delivery;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Primary;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Shared.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Shared.Speedy.Services.Shipment;

namespace CustomCADs.Shared.Speedy.Services.Models.Shipment;

public static class Mapper
{
    public static ShipmentModel ToModel(this ShipmentDto dto)
        => new(
            Recipient: dto.Recipient.ToModel(),
            Service: dto.Service.ToModel(),
            Content: dto.Content.ToModel(),
            Payment: dto.Payment.ToModel(),
            Sender: dto.Sender.ToModel(),
            Id: dto.Id,
            ShipmentNote: dto.ShipmentNote,
            Ref1: dto.Ref1,
            Ref2: dto.Ref2,
            Price: dto.Price.ToModel(),
            Delivery: dto.Delivery.ToModel(),
            PrimaryShipment: dto.PrimaryShipment.ToModel(),
            ReturnShipmentId: dto.ReturnShipmentId,
            RedirectShipmentId: dto.RedirectShipmentId,
            PendingShipment: dto.PendingShipment
        );
}
