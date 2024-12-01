using CustomCADs.Gallery.Application.Common.Exceptions;
using CustomCADs.Gallery.Domain.Carts;
using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Carts.Reads;
using CustomCADs.Gallery.Domain.Common;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.Gallery.Application.Carts.Commands.AddItem;

public class AddCartItemHandler(ICartReads reads, IUnitOfWork uow, IRequestSender sender)
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
            deliverType: req.DeliveryType,
            price: new(price, "BGN"),
            quantity: req.Quantity,
            productId: req.ProductId
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
