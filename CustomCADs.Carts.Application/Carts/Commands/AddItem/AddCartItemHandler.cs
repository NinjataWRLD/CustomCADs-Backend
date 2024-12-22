using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItem;

public sealed class AddCartItemHandler(ICartReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<AddCartItemCommand, CartItemId>
{
    public async Task<CartItemId> Handle(AddCartItemCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId == req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        GetProductPriceByIdQuery productQuery = new(
            Id: req.ProductId
        );
        decimal price = await sender.SendQueryAsync(productQuery, ct).ConfigureAwait(false);

        CartItem item = cart.AddItem(
            productId: req.ProductId,
            quantity: req.Quantity,
            weight: req.Weight,
            delivery: req.Delivery,
            price: price
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
