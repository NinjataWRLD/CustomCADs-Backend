using CustomCADs.Catalog.Application.Products.Queries.Count;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.CountProducts;

public class CountProductsEndpoint(IMessageBus bus) : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("Count");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(User.GetId(), ProductStatus.Unchecked);
        var uncheckedProductsCounts = await bus.InvokeAsync<int>(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Validated };
        var validatedProductsCounts = await bus.InvokeAsync<int>(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Reported };
        var reportedProductsCounts = await bus.InvokeAsync<int>(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Removed };
        var bannedProductsCounts = await bus.InvokeAsync<int>(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(uncheckedProductsCounts, validatedProductsCounts, reportedProductsCounts, bannedProductsCounts);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
