using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;

public class CreatePurchasedCartHandler(IWrites<PurchasedCart> writes, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<CreatePurchasedCartCommand, PurchasedCartId>
{
    public async Task<PurchasedCartId> Handle(CreatePurchasedCartCommand req, CancellationToken ct)
    {
        GetAccountExistsByIdQuery query = new(req.BuyerId);
        bool buyerExists = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        if (!buyerExists)
        {
            throw PurchasedCartNotFoundException.BuyerId(req.BuyerId);
        }
        var cart = PurchasedCart.Create(req.BuyerId);

        await writes.AddAsync(cart, ct).ConfigureAwait(false);
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return cart.Id;
    }
}
