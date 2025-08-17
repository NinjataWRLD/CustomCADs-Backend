using CustomCADs.Speedy.Http.Dtos.Payout;
using CustomCADs.Speedy.Core.Contracts.Payment;

namespace CustomCADs.Speedy.Core.Services.Payment;

internal static class Mapper
{
	internal static PayoutModel ToModel(this PayoutDto dto)
		=> new(
			Date: DateOnly.Parse(dto.Date),
			DocId: dto.DocId,
			DocType: dto.DocType,
			PaymentType: dto.PaymentType,
			Payee: dto.Payee,
			Currency: dto.Currency,
			Amount: dto.Amount,
			Details: [.. dto.Details.Select(d => d.ToModel())]
		);

	internal static PayoutDetailsModel ToModel(this PayoutDetailsDto dto)
		=> new(
			LineNo: dto.LineNo,
			ShipmentId: dto.ShipmentId,
			PickupDate: DateOnly.Parse(dto.PickupDate),
			PrimaryShipmentPickupDate: DateOnly.Parse(dto.PrimaryShipmentPickupDate),
			DeliveryDate: DateOnly.Parse(dto.DeliveryDate),
			Sender: dto.Sender,
			Recipient: dto.Recipient,
			Note: dto.Note,
			Ref1: dto.Ref1,
			Ref2: dto.Ref2,
			Currency: dto.Currency,
			Order: dto.Order,
			Amount: dto.Amount
		);
}
