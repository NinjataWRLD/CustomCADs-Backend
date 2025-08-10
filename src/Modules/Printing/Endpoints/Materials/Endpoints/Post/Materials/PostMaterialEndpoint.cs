using CustomCADs.Printing.Application.Materials.Commands.Internal.Create;
using CustomCADs.Printing.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Printing.Endpoints.Materials.Dtos;
using CustomCADs.Printing.Endpoints.Materials.Endpoints.Get.Single;

namespace CustomCADs.Printing.Endpoints.Materials.Endpoints.Post.Materials;

public sealed class PostMaterialEndpoint(IRequestSender sender)
	: Endpoint<PostMaterialRequest, MaterialResponse>
{
	public override void Configure()
	{
		Post("");
		Group<MaterialsGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Add a Material")
		);
	}

	public override async Task HandleAsync(PostMaterialRequest req, CancellationToken ct)
	{
		MaterialId id = await sender.SendCommandAsync(
			new CreateMaterialCommand(
				Name: req.Name,
				Density: req.Density,
				Cost: req.Cost,
				TextureKey: req.TextureKey,
				TextureContentType: req.TextureContentType
			),
			ct
		).ConfigureAwait(false);

		MaterialDto material = await sender.SendQueryAsync(
			new GetMaterialByIdQuery(id),
			ct
		).ConfigureAwait(false);

		MaterialResponse response = material.ToResponse();
		await SendCreatedAtAsync<GetMaterialEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
	}
}
