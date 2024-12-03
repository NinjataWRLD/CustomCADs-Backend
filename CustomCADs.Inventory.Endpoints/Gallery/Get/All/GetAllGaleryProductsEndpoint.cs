using CustomCADs.Inventory.Application.Products.Queries.GetAll;
using CustomCADs.Inventory.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Inventory.Endpoints.Gallery.Get.All;

public sealed class GetAllGaleryProductsEndpoint(IRequestSender sender)
    : Endpoint<GetAllGaleryProductsRequest, Result<GetAllGaleryProductsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("01. All")
            .WithDescription("See all the Validated Products with Filter, Search, Sort and Pagination options")
        );
    }

    public override async Task HandleAsync(GetAllGaleryProductsRequest req, CancellationToken ct)
    {
        GetAllProductsQuery query = new(
            CategoryId: req.CategoryId is null ? null : new CategoryId(req.CategoryId.Value),
            Status: ProductStatus.Validated,
            Name: req.Name,
            Sorting: req.Sorting,
            Page: req.Page,
            Limit: req.Limit
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetAllGaleryProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToGetAllGaleryProductsResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
