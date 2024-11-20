using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Commands.Cads;

namespace CustomCADs.Cads.Application.Cads.SharedCommandsHandlers;

public class SetCadCoordsHandler(ICadReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCadCoordsCommand>
{
    public async Task Handle(SetCadCoordsCommand req, CancellationToken ct)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        if (req.CamCoordinates is not null)
            cad.SetCamCoordinates(req.CamCoordinates.ToCoordinates());

        if (req.PanCoordinates is not null)
            cad.SetPanCoordinates(req.PanCoordinates.ToCoordinates());

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
