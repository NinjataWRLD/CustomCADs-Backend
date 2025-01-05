using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.AddItem;

public sealed record AddActiveCartItemCommand(
    AccountId BuyerId,
    ProductId ProductId,
    double Weight
) : ICommand<ActiveCartItemId>;
