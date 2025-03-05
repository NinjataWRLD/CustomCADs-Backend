using CustomCADs.Customizations.Application.Common.Exceptions;
using CustomCADs.Customizations.Domain.Materials.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Customizations.Application.Materials.Commands.ChangeTexture;

public class SetMaterialTextureHandler(IMaterialReads reads, IRequestSender sender)
    : ICommandHandler<ChangeMaterialTextureCommand>
{
    public async Task Handle(ChangeMaterialTextureCommand req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw MaterialNotFoundException.ById(req.Id);

        if (req.Key is not null)
        {
            SetImageKeyCommand keyCommand = new(material.TextureId, req.Key);
            await sender.SendCommandAsync(keyCommand, ct).ConfigureAwait(false);
        }
        if (req.ContentType is not null)
        {
            SetImageContentTypeCommand contentTypeCommand = new(material.TextureId, req.ContentType);
            await sender.SendCommandAsync(contentTypeCommand, ct).ConfigureAwait(false);
        }
    }
}
