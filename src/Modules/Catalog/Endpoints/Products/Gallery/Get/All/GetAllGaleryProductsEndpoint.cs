using CustomCADs.Catalog.Application.Products.Queries.Shared.GetAll;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.All;

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
            CategoryId: CategoryId.New(req.CategoryId),
            TagIds: TagId.New(req.TagIds),
            Status: ProductStatus.Validated,
            Name: req.Name,
            Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
            Pagination: new(req.Page, req.Limit)
        );
        Result<GetAllProductsDto> result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        Result<GetAllGaleryProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToGalleryGetAllResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
