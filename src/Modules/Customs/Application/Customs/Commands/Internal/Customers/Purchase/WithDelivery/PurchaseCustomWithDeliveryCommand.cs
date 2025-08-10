using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;

public sealed record PurchaseCustomWithDeliveryCommand(
	CustomId Id,
	string PaymentMethodId,
	string ShipmentService,
	int Count,
	AddressDto Address,
	ContactDto Contact,
	CustomizationId CustomizationId,
	AccountId BuyerId
) : ICommand<PaymentDto>;
