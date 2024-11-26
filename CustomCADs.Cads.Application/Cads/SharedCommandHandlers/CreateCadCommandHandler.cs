using CustomCADs.Cads.Domain.Cads;
using CustomCADs.Cads.Domain.Common;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.Core.Common.TypedIds.Cads;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Cads.Application.Cads.SharedCommandHandlers;

public class CreateCadCommandHandler(IWrites<Cad> writes, IUnitOfWork uow)
    : ICommandHandler<CreateCadCommand, CadId>
{
    public async Task<CadId> Handle(CreateCadCommand req, CancellationToken ct)
    {
        Cad cad = Cad.Create(req.Key, req.ContentType, new(), new());

        await writes.AddAsync(cad, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cad.Id;
    }
}
