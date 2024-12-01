using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Domain.Products.Enums;

namespace CustomCADs.Inventory.Endpoints.Designer.Get.All;

public class GetUncheckedProductsEndpoint(IRequestSender sender)
    : Endpoint<GetUncheckedProductsRequest, GetUncheckedProductsResponse>
{
    public override void Configure()
    {
        Get("");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("01. All Unchecked")
            .WithDescription("See all Unchecked Products with Search, Sorting and Pagination options")
        );
    }

    public override async Task HandleAsync(GetUncheckedProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            Status: ProductStatus.Unchecked,
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Page: req.Page,
            Limit: req.Limit
        );
        var result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetUncheckedProductsResponse response = new(
            Count: result.Count,
            Products: [.. result.Items.Select(p => p.ToGetUncheckedProductsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
