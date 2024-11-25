﻿using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Recent;

public class RecentProductsEndpoint(IRequestSender sender)
    : Endpoint<RecentProductsRequest, IEnumerable<RecentProductsResponse>>
{
    public override void Configure()
    {
        Get("recent");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("3. I want to get my recent Products"));
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Sorting: new(ProductSortingType.UploadDate, SortingDirection.Descending),
            Limit: req.Limit
        );
        GetAllProductsDto dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = dto.Products.Select(p => p.ToRecentProductsResponse());
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
