﻿using CustomCADs.Catalog.Application.Common.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Reported;

public sealed record GetReportedProductsRequest(
    int? CategoryId = null,
    string? Name = null,
    ProductDesignerSortingType SortingType = ProductDesignerSortingType.UploadDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
