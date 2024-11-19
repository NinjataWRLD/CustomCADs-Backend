using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(
    CartId Id,
    CartItemId ItemId
) : ICommand;
