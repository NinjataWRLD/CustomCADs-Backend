using CustomCADs.Cads.Domain.Cads.Entites;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Cads.Application.Cads.IntegrationEventHandlers;

public class CadPathUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
{
    public async Task HandleAsync(CadPathUpdateRequestedIntegrationEvent ie)
    {
        Cad cad = await reads.SingleByIdAsync(ie.Id)
            ?? throw CadNotFoundException.ById(ie.Id);

        cad.SetPath(ie.Path);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
