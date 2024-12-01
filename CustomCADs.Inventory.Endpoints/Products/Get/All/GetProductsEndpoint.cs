﻿using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Inventory.Endpoints.Products.Get.All;

public class GetProductsEndpoint(IRequestSender sender)
    : Endpoint<GetProductsRequest, GetProductsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("10. All")
            .WithDescription("See all your Product with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetProductsResponse response = new(
            result.Count,
            [.. result.Items.Select(p => p.ToGetProductsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
