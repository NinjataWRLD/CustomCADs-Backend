using CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Unchecked;

public sealed class GetUncheckedProductsEndpoint(IRequestSender sender)
    : Endpoint<GetUncheckedProductsRequest, Result<GetUncheckedProductsResponse>>
{
    public override void Configure()
    {
        Get("unchecked");
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
            CategoryId: CategoryId.New(req.CategoryId),
            Name: req.Name,
            Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetUncheckedProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(p => p.ToGetUncheckedProductsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
