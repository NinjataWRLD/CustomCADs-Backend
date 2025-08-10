using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

public sealed record PurchaseActiveCartCommand(
	string PaymentMethodId,
	AccountId BuyerId
) : ICommand<PaymentDto>;
