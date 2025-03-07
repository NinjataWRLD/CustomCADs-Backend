﻿using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;

public sealed class SetCadKeyHandler(ICadReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCadKeyCommand>
{
    public async Task Handle(SetCadKeyCommand req, CancellationToken ct = default)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        cad.SetKey(req.Key);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
