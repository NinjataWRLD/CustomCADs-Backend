using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Commands.AddPurchase;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.Carts.Application.Carts.Commands.PurchaseWithDelivery;

public sealed class PurchaseCartWithDeliveryHandler(ICartReads reads, IUnitOfWork uow, IRequestSender sender, IPaymentService payment)
    : ICommandHandler<PurchaseCartWithDeliveryCommand, string>
{
    public async Task<string> Handle(PurchaseCartWithDeliveryCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.CartId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.CartId);

        if (cart.BuyerId != req.BuyerId)
            throw CartAuthorizationException.ByCartId(cart.Id);

        if (!cart.HasDelivery)
            throw CartItemDeliveryException.ById(cart.Id);

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        int count = cart.TotalDeliveryCount;
        double weight = cart.TotalDeliveryWeight;
        decimal price = cart.TotalCost;

        CreateShipmentCommand shipmentCommand = new(
            Info: new(count, weight, buyer),
            Service: req.ShipmentService,
            Address: req.Address,
            Contact: req.Contact,
            BuyerId: req.BuyerId
        );
        var (ShipmentId, Price) = await sender.SendCommandAsync(shipmentCommand, ct).ConfigureAwait(false);

        cart.SetShipmentId(ShipmentId);
        price += Price;

        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {count} products for a total of {price}$."
        ).ConfigureAwait(false);
        cart.SetPurchasedStatus();

        await uow.SaveChangesAsync(ct).ConfigureAwait(false);

        AddProductPurchaseCommand productCommand = new(
            Ids: [.. cart.Items.Select(i => i.ProductId)]
        );
        await sender.SendCommandAsync(productCommand, ct).ConfigureAwait(false);

        return message;
    }
}
