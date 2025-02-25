using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;

public sealed record AddActiveCartItemCommand(
    AccountId BuyerId,
    double Weight,
    bool ForDelivery,
    ProductId ProductId
) : ICommand<ActiveCartItemId>;
