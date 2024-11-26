using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    UserId BuyerId,
    ICollection<CartItem> Items
);
