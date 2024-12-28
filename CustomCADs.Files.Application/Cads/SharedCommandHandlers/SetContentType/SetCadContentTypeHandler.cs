using CustomCADs.Files.Application.Common.Exceptions;
using CustomCADs.Files.Domain.Cads.Reads;
using CustomCADs.Files.Domain.Common;
using CustomCADs.Shared.Application.Requests.Commands;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetContentType;

public sealed class SetCadContentTypeHandler(ICadReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCadContentTypeCommand>
{
    public async Task Handle(SetCadContentTypeCommand req, CancellationToken ct = default)
    {
        Cad cad = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CadNotFoundException.ById(req.Id);

        cad.SetContentType(req.ContentType);

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
