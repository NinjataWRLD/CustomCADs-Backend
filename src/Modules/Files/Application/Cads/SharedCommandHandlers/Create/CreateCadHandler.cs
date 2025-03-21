﻿using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Abstractions.Requests.Commands;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.Create;

public sealed class CreateCadHandler(IWrites<Cad> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCadCommand, CadId>
{
    public async Task<CadId> Handle(CreateCadCommand req, CancellationToken ct)
    {
        Cad cad = Cad.Create(req.Key, req.ContentType, req.Volume, new(), new());

        await writes.AddAsync(cad, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cad.Id;
    }
}
