using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    UserId BuyerId,
    ICollection<CartItem> Items
);
