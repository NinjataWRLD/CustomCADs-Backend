using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Application.Storage;
using CustomCADs.Shared.IntegrationEvents.Files;

namespace CustomCADs.Files.Application.Cads.IntegrationEventHandlers;

public class ProductDeletedIntegrationEventHandler(ICadReads reads, IWrites<Cad> writes, IUnitOfWork uow, IStorageService storage)
{
    public async Task Handle(ProductDeletedIntegrationEvent ie)
    {
        Cad cad = await reads.SingleByIdAsync(ie.CadId, track: true).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(ie.CadId);

        await storage.DeleteFileAsync(cad.Key).ConfigureAwait(false);

        writes.Remove(cad);
        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
