﻿using CustomCADs.Catalog.Application.Products.Queries.Designer.GetSortings;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("05. Sortings")
            .WithDescription("See all Product Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetProductDesignerSortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
