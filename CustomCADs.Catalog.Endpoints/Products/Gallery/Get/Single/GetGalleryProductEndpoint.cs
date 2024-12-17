using CustomCADs.Catalog.Application.Products.Queries.GalleryGetById;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Get.Single;

public sealed class GetGalleryProductEndpoint(IRequestSender sender)
    : Endpoint<GetGalleryProductRequest, GetGalleryProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("02. Single")
            .WithDescription("See a Validated Product by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetGalleryProductRequest req, CancellationToken ct)
    {
        GalleryGetProductByIdQuery query = new(
            Id: new ProductId(req.Id)
        );
        GalleryGetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetGalleryProductResponse response = product.ToGetGalleryProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
