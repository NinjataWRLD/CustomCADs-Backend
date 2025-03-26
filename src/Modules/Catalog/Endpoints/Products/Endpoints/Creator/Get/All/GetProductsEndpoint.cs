﻿using CustomCADs.Catalog.Application.Products.Queries.Internal.Shared.GetAll;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.All;

public sealed class GetProductsEndpoint(IRequestSender sender)
    : Endpoint<GetProductsRequest, Result<GetProductsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("All")
            .WithDescription("See all your Product with Filter, Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetAccountId(),
            CategoryId: CategoryId.New(req.CategoryId),
            Name: req.Name,
            Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(p => p.ToCreatorGetAllResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
