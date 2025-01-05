using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItemWithDelivery;

public sealed record AddActiveCartItemWithDeliveryCommand(
    AccountId BuyerId,
    double Weight,
    ProductId ProductId
) : ICommand<ActiveCartItemId>;
