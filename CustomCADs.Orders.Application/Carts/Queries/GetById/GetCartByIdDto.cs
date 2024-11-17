using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    UserId BuyerId,
    ICollection<GalleryOrder> Orders
);
