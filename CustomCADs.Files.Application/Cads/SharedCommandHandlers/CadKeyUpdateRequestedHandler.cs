using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers;

public sealed class CadKeyUpdateRequestedHandler(ICadReads reads, IUnitOfWork uow)
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
