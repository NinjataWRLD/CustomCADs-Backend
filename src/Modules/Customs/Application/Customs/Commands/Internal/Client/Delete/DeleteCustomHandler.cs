using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Delete;

public sealed class DeleteCustomHandler(ICustomReads reads, IWrites<Custom> writes, IUnitOfWork uow)
    : ICommandHandler<DeleteCustomCommand>
{
    public async Task Handle(DeleteCustomCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        if (custom.BuyerId != req.BuyerId)
            throw CustomAuthorizationException<Custom>.ById(req.Id);

        writes.Remove(custom);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}
