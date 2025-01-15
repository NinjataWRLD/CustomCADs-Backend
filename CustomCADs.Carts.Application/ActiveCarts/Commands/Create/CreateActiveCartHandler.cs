using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Create;

public sealed class CreateActiveCartHandler(IActiveCartReads reads, IWrites<ActiveCart> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreateActiveCartCommand, ActiveCartId>
{
    public async Task<ActiveCartId> Handle(CreateActiveCartCommand req, CancellationToken ct)
    {
        bool cartExists = await reads.ExistsByBuyerIdAsync(req.BuyerId, ct: ct).ConfigureAwait(false);
        if (cartExists)
        {
            throw ActiveCartAlreadyExistsException.ByBuyerId(req.BuyerId);
        }

        GetAccountExistsByIdQuery query = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!buyerExists)
        {
            throw ActiveCartNotFoundException.BuyerId(req.BuyerId);
        }

        ActiveCart cart = req.ToCart();

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
