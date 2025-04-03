using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.SetDelivery;

public class SetCustomDeliveryHandler(ICustomReads reads, IUnitOfWork uow)
    : ICommandHandler<SetCustomDeliveryCommand>
{
    public async Task Handle(SetCustomDeliveryCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        custom.SetDelivery(req.Value);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
