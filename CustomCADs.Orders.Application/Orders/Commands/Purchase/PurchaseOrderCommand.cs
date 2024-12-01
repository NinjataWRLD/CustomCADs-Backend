using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Orders.Application.Orders.Commands.Purchase;

public record PurchaseOrderCommand(
    string PaymentMethodId,
    OrderId OrderId,
    AccountId BuyerId
) : ICommand<string>;
