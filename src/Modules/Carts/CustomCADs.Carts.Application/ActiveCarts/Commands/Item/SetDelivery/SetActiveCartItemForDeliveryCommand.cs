using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Item.SetDelivery;

public record SetActiveCartItemForDeliveryCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId,
    bool Value
) : ICommand;
