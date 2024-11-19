using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Commands.RemoveOrder;

public record RemoveCartOrderCommand(
    CartId Id,
    GalleryOrderId OrderId
) : ICommand;
