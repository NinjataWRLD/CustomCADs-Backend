using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProducts;
public class GetProductsEndpoint(IMediator mediator) : Endpoint<GetProductsRequest, GetProductsResponse>
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
        GetAllProductsDto result = await mediator.Send(query, ct).ConfigureAwait(false);

        GetProductsResponse response = new(result.Count, result.Products.Adapt<GetProductsDto[]>());
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
