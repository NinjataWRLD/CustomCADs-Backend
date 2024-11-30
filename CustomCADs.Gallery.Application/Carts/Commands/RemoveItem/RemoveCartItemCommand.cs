using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(
    CartId Id,
    CartItemId ItemId,
    AccountId BuyerId
) : ICommand;
