using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Shared.Core.Bases.Events;

namespace CustomCADs.Carts.Domain.ActiveCarts.Events;

public record ActiveCartPurchasedDomainEvent(
    PurchasedCartId Id,
    ActiveCartItem[] Items
) : BaseDomainEvent;
