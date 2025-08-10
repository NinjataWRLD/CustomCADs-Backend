using CustomCADs.Printing.Application.Materials.Commands.Internal.Delete;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Delete;

public sealed class DeleteMaterialEndpoint(IRequestSender sender)
	: Endpoint<DeleteMaterialRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("Delete")
			.WithDescription("Delete a Material")
		);
	}

	public override async Task HandleAsync(DeleteMaterialRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DeleteMaterialCommand(
				Id: MaterialId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
