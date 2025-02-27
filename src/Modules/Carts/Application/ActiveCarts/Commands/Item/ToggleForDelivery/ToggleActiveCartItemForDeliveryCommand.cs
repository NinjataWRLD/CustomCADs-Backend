using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.ToggleForDelivery;

public record ToggleActiveCartItemForDeliveryCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId
) : ICommand;
