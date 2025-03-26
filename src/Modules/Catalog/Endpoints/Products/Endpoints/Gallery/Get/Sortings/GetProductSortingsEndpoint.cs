using CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetSortings;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Get.Sortings;

public sealed class GetProductSortingsEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<string[]>
{
    public override void Configure()
    {
        Get("sortings");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("05. Sortings")
            .WithDescription("See all Product Sorting types")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        GetProductGallerySortingsQuery query = new();
        string[] result = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendOkAsync(result).ConfigureAwait(false);
    }
}
