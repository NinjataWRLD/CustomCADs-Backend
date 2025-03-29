﻿using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.ActiveCarts.Dtos;

public record ActiveCartItemDto(
    int Quantity,
    bool ForDelivery,
    string BuyerName,
    AccountId BuyerId,
    ProductId ProductId,
    CustomizationId? CustomizationId
);