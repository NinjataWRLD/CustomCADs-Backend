using CustomCADs.Cads.Application.Cads.Exceptions;
using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Cads.Reads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Cads.Application.Cads.SharedCommandHandlers;

public class CadKeyUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
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
