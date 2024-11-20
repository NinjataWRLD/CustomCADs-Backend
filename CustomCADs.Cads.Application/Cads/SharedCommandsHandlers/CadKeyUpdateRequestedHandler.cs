using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Cads.Domain.Common.Exceptions.Cads;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Commands.Cads;

namespace CustomCADs.Cads.Application.Cads.SharedCommandsHandlers;

public class CadKeyUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCadKeyCommand>
{
    public async Task Handle(SetCadKeyCommand req, CancellationToken ct = default)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw CadNotFoundException.ById(req.Id);

        cad.SetKey(req.Key);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
