using CustomCADs.Catalog.Application.Products.Queries.Count;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Endpoints.Products.CountProducts;

public class CountProductsEndpoint(IMediator mediator) : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("count");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(User.GetAccountId(), ProductStatus.Unchecked);
        var uncheckedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Validated };
        var validatedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Reported };
        var reportedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Removed };
        var bannedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(uncheckedProductsCounts, validatedProductsCounts, reportedProductsCounts, bannedProductsCounts);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
