using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;

public sealed class AcceptCustomHandler(ICustomReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<AcceptCustomCommand>
{
    public async Task Handle(AcceptCustomCommand req, CancellationToken ct)
    {
        Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CustomNotFoundException<Custom>.ById(req.Id);

        GetAccountExistsByIdQuery designerQuery = new(req.DesignerId);
        bool designerExists = await sender.SendQueryAsync(designerQuery, ct).ConfigureAwait(false);
        if (!designerExists)
            throw CustomNotFoundException<Custom>.ById(req.DesignerId, "User");

        custom.Accept(req.DesignerId);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);
    }
}

