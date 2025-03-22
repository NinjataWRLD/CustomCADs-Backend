﻿using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.ApplicationEvents.Files;

namespace CustomCADs.Files.Application.Cads.ApplicationEventHandlers;

public class ProductDeletedHandler(ICadReads reads, IWrites<Cad> writes, IUnitOfWork uow, IStorageService storage)
{
    public async Task Handle(ProductDeletedApplicationEvent ae)
    {
        Cad cad = await reads.SingleByIdAsync(ae.CadId, track: true).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(ae.CadId);

        await storage.DeleteFileAsync(cad.Key).ConfigureAwait(false);

        writes.Remove(cad);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
