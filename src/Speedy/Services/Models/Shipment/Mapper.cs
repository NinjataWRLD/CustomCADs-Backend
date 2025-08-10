using CustomCADs.Speedy.API.Dtos.Shipment;
using CustomCADs.Speedy.Services.Models.Shipment;
using CustomCADs.Speedy.Services.Models.Shipment.Content;
using CustomCADs.Speedy.Services.Models.Shipment.Delivery;
using CustomCADs.Speedy.Services.Models.Shipment.Payment;
using CustomCADs.Speedy.Services.Models.Shipment.Primary;
using CustomCADs.Speedy.Services.Models.Shipment.Recipient;
using CustomCADs.Speedy.Services.Models.Shipment.Sender;
using CustomCADs.Speedy.Services.Models.Shipment.Service;
using CustomCADs.Speedy.Services.Shipment;

namespace CustomCADs.Speedy.Services.Models.Shipment;

internal static class Mapper
{
	internal static ShipmentModel ToModel(this ShipmentDto dto, string phone)
		=> new(
			Recipient: dto.Recipient.ToModel(),
			Service: dto.Service.ToModel(),
			Content: dto.Content.ToModel(),
			Payment: dto.Payment.ToModel(),
			Sender: dto.Sender.ToModel(phone),
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
