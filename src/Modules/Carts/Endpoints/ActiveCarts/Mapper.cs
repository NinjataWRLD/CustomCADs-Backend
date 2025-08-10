using CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Get.CalculateShipment;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Carts.Endpoints.ActiveCarts;

internal static class Mapper
{
	internal static ActiveCartItemResponse ToResponse(this ActiveCartItemDto item)
		=> new(
			Quantity: item.Quantity,
			ForDelivery: item.ForDelivery,
			AddedAt: item.AddedAt,
			ProductId: item.ProductId.Value,
			CustomizationId: item.CustomizationId?.Value
		);

	internal static CalculateActiveCartShipmentResponse ToResponse(this CalculateShipmentDto calculation)
		=> new(
			Service: calculation.Service,
			Total: calculation.Total,
			Currency: calculation.Currency,
			PickupDate: calculation.PickupDate,
			DeliveryDeadline: calculation.DeliveryDeadline
		);

	internal static PaymentResponse ToResponse(this PaymentDto payment)
		=> new(
			ClientSecret: payment.ClientSecret,
			Message: payment.Message
		);
}
