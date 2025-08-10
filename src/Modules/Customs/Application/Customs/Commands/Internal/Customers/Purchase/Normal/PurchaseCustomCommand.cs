using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

public sealed record PurchaseCustomCommand(
	CustomId Id,
	string PaymentMethodId,
	AccountId BuyerId
) : ICommand<PaymentDto>;
