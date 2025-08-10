using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

public sealed record PurchaseActiveCartWithDeliveryCommand(
	string PaymentMethodId,
	string ShipmentService,
	AddressDto Address,
	ContactDto Contact,
	AccountId BuyerId
) : ICommand<PaymentDto>;
