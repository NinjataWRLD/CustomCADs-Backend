using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Cads.Application.Cads.IntegrationEventHandlers;

public class CadKeyUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
{
    public async Task HandleAsync(CadKeyUpdateRequestedIntegrationEvent ie)
    {
        Cad cad = await reads.SingleByIdAsync(ie.Id)
            ?? throw CadNotFoundException.ById(ie.Id);

        cad.SetKey(ie.Key);

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
