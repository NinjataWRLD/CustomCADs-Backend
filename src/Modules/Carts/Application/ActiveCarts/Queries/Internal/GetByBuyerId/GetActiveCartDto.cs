﻿using CustomCADs.Carts.Application.ActiveCarts.Dtos;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetByBuyerId;

public record GetActiveCartDto(
    ActiveCartId Id,
    string BuyerName,
    ICollection<ActiveCartItemDto> Items
);
