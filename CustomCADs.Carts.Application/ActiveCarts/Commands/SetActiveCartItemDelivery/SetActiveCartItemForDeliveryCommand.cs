using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.SetActiveCartItemDelivery;

public record SetActiveCartItemForDeliveryCommand(
    AccountId BuyerId,
    ActiveCartItemId ItemId,
    bool Value
) : ICommand;
