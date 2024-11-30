﻿using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Gallery.Application.Carts.Queries.GetById;

public record GetCartByIdQuery(
    CartId Id,
    AccountId BuyerId
) : IQuery<GetCartByIdDto>;
