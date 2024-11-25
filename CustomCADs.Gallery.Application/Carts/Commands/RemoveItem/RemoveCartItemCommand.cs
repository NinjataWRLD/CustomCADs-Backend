using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(
    CartId Id,
    CartItemId ItemId
) : ICommand;
