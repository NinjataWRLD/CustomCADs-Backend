﻿using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Wolverine;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.RecentProducts;

public class RecentProductsEndpoint(IMessageBus bus) : Endpoint<RecentProductsRequest, IEnumerable<RecentProductsResponse>>
{
    public override void Configure()
    {
        Get("Recent");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(RecentProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetId(),
            Sorting: nameof(ProductSorting.Newest),
            Limit: req.Limit
        );
        var dto = await bus.InvokeAsync<GetAllProductsDto>(query, ct).ConfigureAwait(false);

        RecentProductsResponse[] response = dto.Products.Select(p => new RecentProductsResponse()
        {
            Id = p.Id,
            Name = p.Name,
            Status = p.Status,
            UploadDate = p.UploadDate.ToString(DateFormatString),
        }).ToArray();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
