using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.AddTag;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Patch.AddTag;

using static Constants.Roles;

public class AddProductTagEndpoint(IRequestSender sender)
	: Endpoint<AddProductTagRequest>
{
	public override void Configure()
	{
		Patch("tags/add");
		Group<GalleryGroup>();
		Roles(Admin);
		Description(d => d
			.WithSummary("Add Tag")
			.WithDescription("Adds a Tag to a Product")
		);
	}

	public override async Task HandleAsync(AddProductTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new AddProductTagCommand(
				Id: ProductId.New(req.Id),
				TagId: TagId.New(req.TagId)
			),
			ct
		).ConfigureAwait(false);
	}
}
