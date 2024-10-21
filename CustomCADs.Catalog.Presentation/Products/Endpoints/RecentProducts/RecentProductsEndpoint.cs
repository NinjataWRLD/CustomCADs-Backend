using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Mapster;
using Wolverine;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.RecentProducts;

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

        var response = dto.Products.Adapt<RecentProductsResponse[]>();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
