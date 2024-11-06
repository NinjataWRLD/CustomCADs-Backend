using CustomCADs.Catalog.Application.Products.Queries.GetAll;

namespace CustomCADs.Catalog.Endpoints.Products.GetProducts;

using static Constants;

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
            CreatorId: User.GetAccountId(),
            Category: req.Category,
            Name: req.Name,
            Sorting: req.Sorting ?? string.Empty,
            Page: req.Page,
            Limit: req.Limit
        );
        GetAllProductsDto result = await mediator.Send(query, ct).ConfigureAwait(false);

        var products = result.Products.Select(p => new GetProductsDto(
            Id: p.Id,
            Name: p.Name,
            UploadDate: p.UploadDate.ToString(DateFormatString),
            ImagePath: p.ImagePath,
            CreatorName: p.CreatorName,
            Category: new()
            {
                Id = p.Category.Id,
                Name = p.Category.Name,
            }
        )).ToArray();
        GetProductsResponse response = new(result.Count, products);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
