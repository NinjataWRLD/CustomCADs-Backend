﻿using CustomCADs.Customizations.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Customizations.Application.Materials.Commands.Internal.ChangeTexture;

public class SetMaterialTextureHandler(IMaterialReads reads, IRequestSender sender)
    : ICommandHandler<ChangeMaterialTextureCommand>
{
    public async Task Handle(ChangeMaterialTextureCommand req, CancellationToken ct)
    {
        Material material = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Material>.ById(req.Id);

        if (req.Key is not null)
        {
            await sender.SendCommandAsync(
                new SetImageKeyCommand(material.TextureId, req.Key),
                ct
            ).ConfigureAwait(false);
        }
        if (req.ContentType is not null)
        {
            await sender.SendCommandAsync(
                new SetImageContentTypeCommand(material.TextureId, req.ContentType),
                ct
            ).ConfigureAwait(false);
        }
    }
}
