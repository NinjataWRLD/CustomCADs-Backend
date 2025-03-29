using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Patch.RemoveTag;

using static Constants.Roles;

public class RemoveProductTagEndpoint(IRequestSender sender)
    : Endpoint<RemoveProductTagRequest>
{
    public override void Configure()
    {
        Patch("tags/remove");
        Group<GalleryGroup>();
        Roles(Admin);
        Description(d => d
            .WithSummary("Remove Tag")
            .WithDescription("Removes a Tag from a Product")
        );
    }

    public override async Task HandleAsync(RemoveProductTagRequest req, CancellationToken ct)
    {
        RemoveProductTagCommand command = new(
            Id: ProductId.New(req.Id),
            TagId: TagId.New(req.TagId)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);
    }
}
