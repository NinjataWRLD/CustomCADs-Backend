using CustomCADs.Catalog.Application.Products.Queries.Count;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.CountProducts;

public class CountProductsEndpoint(IMediator mediator) : EndpointWithoutRequest<CountProductsResponse>
{
    public override void Configure()
    {
        Get("Count");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        ProductsCountQuery query = new(User.GetId(), ProductStatus.Unchecked);
        int uncheckedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Validated };
        int validatedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Reported };
        int reportedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        query = query with { Status = ProductStatus.Removed };
        int bannedProductsCounts = await mediator.Send(query, ct).ConfigureAwait(false);

        CountProductsResponse response = new(uncheckedProductsCounts, validatedProductsCounts, reportedProductsCounts, bannedProductsCounts);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
