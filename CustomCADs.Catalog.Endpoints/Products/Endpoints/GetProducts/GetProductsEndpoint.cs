using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Shared.Presentation;
using FastEndpoints;
using Mapster;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.GetProducts;

public class GetProductsEndpoint(IMessageBus bus) : Endpoint<GetProductsRequest, GetProductsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CreatorId: User.GetId(),
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting ?? string.Empty,
            Page: req.Page,
            Limit: req.Limit
        );
        var result = await bus.InvokeAsync<GetAllProductsDto>(query, ct).ConfigureAwait(false);

        GetProductsResponse response = new(result.Count, result.Products.Adapt<GetProductsDto[]>());
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
