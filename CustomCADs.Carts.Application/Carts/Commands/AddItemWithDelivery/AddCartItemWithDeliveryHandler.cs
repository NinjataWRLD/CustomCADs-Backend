using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Carts.Application.Carts.Commands.AddItemWithDelivery;

public sealed class AddCartItemWithDeliveryHandler(ICartReads reads, IUnitOfWork uow, IRequestSender sender)
    : ICommandHandler<AddCartItemWithDeliveryCommand, CartItemId>
{
    public async Task<CartItemId> Handle(AddCartItemWithDeliveryCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.Id);

        if (cart.BuyerId != req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(req.Id);
        }

        GetProductPriceByIdQuery productQuery = new(
            Id: req.ProductId
        );
        decimal price = await sender.SendQueryAsync(productQuery, ct).ConfigureAwait(false);

        int quantity = 1;
        double weight = req.Weight;
        CartItem item = cart.AddItem(
            productId: req.ProductId,
            quantity: quantity,
            weight: weight,
            delivery: true,
            price: price
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
