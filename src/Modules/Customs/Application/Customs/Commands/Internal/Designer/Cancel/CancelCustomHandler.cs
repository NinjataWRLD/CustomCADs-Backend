using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

public sealed class CancelCustomHandler(ICustomReads reads, IUnitOfWork uow)
    : ICommandHandler<CancelCustomCommand>
{
    public async Task Handle(CancelCustomCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (req.DesignerId != custom.AcceptedCustom?.DesignerId)
            throw CustomAuthorizationException<Custom>.ById(req.Id);

        custom.Cancel();
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

