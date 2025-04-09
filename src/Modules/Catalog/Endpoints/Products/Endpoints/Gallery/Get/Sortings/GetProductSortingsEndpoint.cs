using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("Sortings")
            .WithDescription("See all Product Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string[] result = await sender.SendQueryAsync(
            new GetProductGallerySortingsQuery(),
            ct
        ).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
