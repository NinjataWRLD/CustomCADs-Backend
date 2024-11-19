using CustomCADs.Cads.Domain.Cads.Entites;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.IntegrationEvents.Catalog;

namespace CustomCADs.Cads.Application.Cads.IntegrationEventHandlers;

public class CadCoordsUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
{
    public async Task HandleAsync(CadCoordsUpdateRequestedIntegrationEvent ie)
    {
        Cad cad = await reads.SingleByIdAsync(ie.Id)
            ?? throw CadNotFoundException.ById(ie.Id);

        if (ie.CamCoordinates is not null)
        {
            cad.SetCamCoordinates(ie.CamCoordinates.ToCoordinates());
        }

        if (ie.PanCoordinates is not null)
        {
            cad.SetPanCoordinates(ie.PanCoordinates.ToCoordinates());
        }

        await uow.SaveChangesAsync().ConfigureAwait(false);
    }
}
