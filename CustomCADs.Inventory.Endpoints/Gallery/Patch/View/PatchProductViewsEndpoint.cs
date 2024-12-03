using CustomCADs.Inventory.Application.Products.Commands.AddView;

namespace CustomCADs.Inventory.Endpoints.Gallery.Patch.View;

public sealed class PatchProductViewsEndpoint(IRequestSender sender)
    : Endpoint<PatchProductViewsRequest>
{
    public override void Configure()
    {
        Patch("{id}/view");
        Group<GalleryGroup>();
        Description(d => d
            .WithSummary("03. View")
            .WithDescription("Increment the view count of a Product by specifying its Id")
        );
    }

    public override async Task HandleAsync(PatchProductViewsRequest req, CancellationToken ct)
    {
        AddProductViewCommand command = new(
            Id: new ProductId(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
