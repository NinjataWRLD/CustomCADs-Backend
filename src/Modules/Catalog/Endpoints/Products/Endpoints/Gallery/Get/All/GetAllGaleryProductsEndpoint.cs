using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;
using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.All;

public sealed class GetAllGaleryProductsEndpoint(IRequestSender sender)
    : Endpoint<GetAllGaleryProductsRequest, Result<GetAllGaleryProductsResponse>>
{
    public override void Configure()
    {
        Get("");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("All")
            .WithDescription("See all the Validated Products with Filter, Search, Sort and Pagination options")
        );
    }

    public override async Task HandleAsync(GetAllGaleryProductsRequest req, CancellationToken ct)
    {
        Result<GalleryGetAllProductsDto> result = await sender.SendQueryAsync(
            new GalleryGetAllProductsQuery(
                BuyerId: User.GetAccountId(),
                CategoryId: CategoryId.New(req.CategoryId),
                TagIds: TagId.New(req.TagIds),
                Name: req.Name,
                Sorting: new(req.SortingType.ToBase(), req.SortingDirection),
                Pagination: new(req.Page, req.Limit)
            ),
            ct
        ).ConfigureAwait(false);

        Result<GetAllGaleryProductsResponse> response = new(
            Count: result.Count,
            Items: [.. result.Items.Select(i => i.ToResponse())]
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
