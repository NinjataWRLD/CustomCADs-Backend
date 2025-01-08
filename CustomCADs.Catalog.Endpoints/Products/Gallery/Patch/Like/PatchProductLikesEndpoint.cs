using CustomCADs.Catalog.Application.Products.Commands.AddLike;

namespace CustomCADs.Catalog.Endpoints.Products.Gallery.Patch.Like;

using static Constants.Roles;

public sealed class PatchProductLikesEndpoint(IRequestSender sender)
    : Endpoint<PatchProductLikesRequest>
{
    public override void Configure()
    {
        Patch("{id}/like");
        Group<GalleryGroup>();
        Roles(Client);
        Description(d => d
            .WithSummary("04. Like")
            .WithDescription("Increment the like count of a Product by specifying its Id")
        );
    }

    public override async Task HandleAsync(PatchProductLikesRequest req, CancellationToken ct)
    {
        AddProductLikeCommand command = new(
            Id: ProductId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
