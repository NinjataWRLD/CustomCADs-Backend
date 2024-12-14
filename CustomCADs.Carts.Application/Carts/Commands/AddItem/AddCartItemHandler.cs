using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Products.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

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

        ShipmentId? shipmentId = null;
        if (req.Delivery)
        {
            CreateShipmentCommand shipmentCommand = new(req.BuyerId);
            shipmentId = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);
        }

        CartItem item = cart.AddItem(
            price: price,
            quantity: req.Quantity,
            productId: req.ProductId,
            shipmentId: shipmentId
        );
        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        return item.Id;
    }
}
