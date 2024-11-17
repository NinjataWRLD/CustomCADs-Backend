using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Commands.RemvoeOrder;

public record RemoveCartOrderCommand(
    CartId Id,
    GalleryOrderId OrderId,
    UserId BuyerId
) : ICommand;
