﻿using CustomCADs.Shared.Core.Bases.Events;
using CustomCADs.Shared.Core.Common.Dtos;

namespace CustomCADs.Carts.Domain.PurchasedCarts.Events;

public record CartPurchasedWithDeliveryDomainEvent(
    PurchasedCartId CartId,
    string ShipmentService,
    double Weight,
    int Count,
    AddressDto Address,
    ContactDto Contact
) : BaseDomainEvent;
