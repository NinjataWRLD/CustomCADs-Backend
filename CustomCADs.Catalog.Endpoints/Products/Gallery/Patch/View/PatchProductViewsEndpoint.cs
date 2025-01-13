using CustomCADs.Catalog.Application.Products.Commands.AddView;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Patch.View;

public sealed class PatchProductViewsEndpoint(IRequestSender sender)
    : Endpoint<PatchProductViewsRequest>
{
    public override void Configure()
    {
        Patch("view");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("03. View")
            .WithDescription("Increment the view count of a Product")
        );
    }

    public override async Task HandleAsync(PatchProductViewsRequest req, CancellationToken ct)
    {
        AddProductViewCommand command = new(
            Id: ProductId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
