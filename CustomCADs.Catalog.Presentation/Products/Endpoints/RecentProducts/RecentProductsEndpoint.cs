using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.RecentProducts;

public class RecentProductsEndpoint(IMediator mediator) : Endpoint<RecentProductsRequest, IEnumerable<RecentProductsResponse>>
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
        GetAllProductsDto dto = await mediator.Send(query, ct).ConfigureAwait(false);

        var response = dto.Products.Adapt<RecentProductsResponse[]>();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
