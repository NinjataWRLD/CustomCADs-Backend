using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Create;

public sealed class CreateActiveCartHandler(IActiveCartReads reads, IWrites<ActiveCart> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateActiveCartCommand, ActiveCartId>
{
    public async Task<ActiveCartId> Handle(CreateActiveCartCommand req, CancellationToken ct)
    {
        bool cartExists = await reads.ExistsByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
        if (cartExists)
            throw new CustomException($"Active cart for buyer: {req.BuyerId} already exists.");

        GetAccountExistsByIdQuery query = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!buyerExists)
            throw CustomNotFoundException<ActiveCart>.ById(req.BuyerId, "User");

        ActiveCart cart = req.ToCart();

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
