﻿using CustomCADs.Shared.Core.Common.TypedIds.Account;

namespace CustomCADs.Inventory.Application.Products.Queries.GetImageUrlGet;

public record GetProductImagePresignedUrlGetQuery(
    ProductId Id,
    UserId CreatorId
) : IQuery<GetProductImagePresignedUrlGetDto>;
