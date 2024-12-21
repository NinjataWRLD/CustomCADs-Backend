using CustomCADs.Catalog.Application.Products.Queries.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.All;

public sealed class GetUncheckedProductsEndpoint(IRequestSender sender)
    : Endpoint<GetUncheckedProductsRequest, Result<GetUncheckedProductsDto>>
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
            CategoryId: req.CategoryId is null ? null : new CategoryId(req.CategoryId.Value),
            Name: req.Name,
            Sorting: new(req.SortingType, req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        var result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetUncheckedProductsDto> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(p => p.ToGetUncheckedProductsDto())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
