using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.Normal;

public sealed record PurchaseCustomCommand(
    CustomId Id,
    string PaymentMethodId,
    AccountId BuyerId
) : ICommand<string>;
