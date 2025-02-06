using CustomCADs.Catalog.Application.Products.Queries.Gallery.GetSortings;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Sortings;

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
