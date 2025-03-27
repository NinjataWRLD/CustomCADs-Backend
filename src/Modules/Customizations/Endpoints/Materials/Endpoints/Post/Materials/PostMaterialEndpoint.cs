using CustomCADs.Customizations.Application.Materials.Commands.Internal.Create;
using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Customizations.Endpoints.Materials.Dtos;
using CustomCADs.Customizations.Endpoints.Materials.Endpoints.Get.Single;

namespace CustomCADs.Customizations.Endpoints.Materials.Endpoints.Post.Materials;

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
        CreateMaterialCommand command = new(
            Name: req.Name,
            Density: req.Density,
            Cost: req.Cost,
            TextureKey: req.TextureKey,
            TextureContentType: req.TextureContentType
        );
        MaterialId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetMaterialByIdQuery query = new(id);
        MaterialDto material = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        MaterialResponse response = material.ToResponse();
        await SendCreatedAtAsync<GetMaterialEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
