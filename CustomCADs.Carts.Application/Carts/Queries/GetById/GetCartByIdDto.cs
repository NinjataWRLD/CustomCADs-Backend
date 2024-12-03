using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Carts.Application.Carts.Queries.GetById;

public record GetCartByIdDto(
    CartId Id,
    decimal Total,
    DateTime PurchaseDate,
    AccountId BuyerId,
    ICollection<CartItem> Items
);
