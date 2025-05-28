using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

public sealed record PurchaseCustomCommand(
	CustomId Id,
	string PaymentMethodId,
	AccountId BuyerId
) : ICommand<PaymentDto>;
