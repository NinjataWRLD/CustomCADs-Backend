﻿using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Files.Domain.Images.Reads;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.SetContentType;

public sealed class SetImageContentTypeHandler(IImageReads reads, IUnitOfWork uow)
    : ICommandHandler<SetImageContentTypeCommand>
{
    public async Task Handle(SetImageContentTypeCommand req, CancellationToken ct = default)
    {
        Image image = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw ImageNotFoundException.ById(req.Id);

        image.SetContentType(req.ContentType);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
