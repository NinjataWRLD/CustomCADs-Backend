using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryCommand(
    AccountId BuyerId,
    ProductId ProductId
) : ICommand;
