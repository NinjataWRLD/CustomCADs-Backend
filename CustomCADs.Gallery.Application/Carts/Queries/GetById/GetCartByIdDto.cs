using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    AccountId BuyerId,
    ICollection<CartItem> Items
);
