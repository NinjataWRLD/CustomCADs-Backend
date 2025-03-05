using CustomCADs.Customizations.Application.Materials.Commands.ChangeTexture;
using CustomCADs.Customizations.Application.Materials.Commands.Edit;

namespace CustomCADs.Customizations.Endpoints.Materials.Put.Materials;

public sealed class PutMaterialEndpoint(IRequestSender sender)
    : Endpoint<PutMaterialRequest>
{
    public override void Configure()
    {
        Put("");
        Group<MaterialsGroup>();
        Description(d => d
            .WithSummary("4. Edit")
            .WithDescription("Edit a Material")
        );
    }

    public override async Task HandleAsync(PutMaterialRequest req, CancellationToken ct)
    {
        EditMaterialCommand materialCommand = new(
            Id: MaterialId.New(req.Id),
            Name: req.Name,
            Density: req.Density,
            Cost: req.Cost
        );
        await sender.SendCommandAsync(materialCommand, ct).ConfigureAwait(false);
        
        ChangeMaterialTextureCommand textureCommand = new(
            Id: MaterialId.New(req.Id),
            Key: req.TextureKey,
            ContentType: req.TextureContentType
        );
        await sender.SendCommandAsync(textureCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
