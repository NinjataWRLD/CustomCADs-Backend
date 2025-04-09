using CustomCADs.Customizations.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.Create;

public class CreateMaterialHandler(IWrites<Material> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateMaterialCommand, MaterialId>
{
    public async Task<MaterialId> Handle(CreateMaterialCommand req, CancellationToken ct)
    {
        ImageId textureId = await sender.SendCommandAsync(
            new CreateImageCommand(
                Key: req.TextureKey,
                ContentType: req.TextureContentType
            ),
            ct
        ).ConfigureAwait(false);

        var material = Material.Create(
            name: req.Name,
            density: req.Density,
            cost: req.Cost,
            textureId: textureId
        );

        await writes.AddAsync(material, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return material.Id;
    }
}
