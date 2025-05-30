using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Delete;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Delete;

public sealed class DeleteProductEndpoint(IRequestSender sender)
	: Endpoint<DeleteProductRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<CreatorGroup>();
		Description(d => d
			.WithSummary("Delete")
			.WithDescription("Delete your Product")
		);
	}

	public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DeleteProductCommand(
				Id: ProductId.New(req.Id),
				CreatorId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
