using CustomCADs.Customizations.Application.Materials.Commands.Internal.ChangeTexture;
using CustomCADs.Customizations.Application.Materials.Commands.Internal.Edit;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Put.Materials;

public sealed class PutMaterialEndpoint(IRequestSender sender)
	: Endpoint<PutMaterialRequest>
{
	public override void Configure()
	{
		Put("");
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("Edit")
			.WithDescription("Edit a Material")
		);
	}

	public override async Task HandleAsync(PutMaterialRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new EditMaterialCommand(
				Id: MaterialId.New(req.Id),
				Name: req.Name,
				Density: req.Density,
				Cost: req.Cost
			),
			ct
		).ConfigureAwait(false);

		await sender.SendCommandAsync(
			new ChangeMaterialTextureCommand(
				Id: MaterialId.New(req.Id),
				Key: req.TextureKey,
				ContentType: req.TextureContentType
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
