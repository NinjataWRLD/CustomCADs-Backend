using CustomCADs.Carts.Application.Common.Exceptions;
using CustomCADs.Carts.Domain.Carts;
using CustomCADs.Carts.Domain.Carts.Reads;
using CustomCADs.Shared.Application.Delivery;
using CustomCADs.Shared.Application.Delivery.Dtos;
using CustomCADs.Shared.Application.Payment;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Commands.AddPurchase;

namespace CustomCADs.Carts.Application.Carts.Commands.Purchase;

public sealed class PurchaseCartHandler(ICartReads reads, IRequestSender sender, IPaymentService payment, IDeliveryService delivery)
    : ICommandHandler<PurchaseCartCommand, string>
{
    public async Task<string> Handle(PurchaseCartCommand req, CancellationToken ct)
    {
        Cart cart = await reads.SingleByIdAsync(req.CartId, track: false, ct: ct).ConfigureAwait(false)
            ?? throw CartNotFoundException.ById(req.CartId);

        if (cart.BuyerId != req.BuyerId)
        {
            throw CartAuthorizationException.ByCartId(cart.Id);
        }

        GetUsernameByIdQuery buyerQuery = new(cart.BuyerId);
        string buyer = await sender.SendQueryAsync(buyerQuery, ct).ConfigureAwait(false);

        decimal price = 0m;
        int count = cart.Items.Count;
        if (cart.Delivery)
        {
            int weight = 5; // integrate calculations
            ShipmentDto shipment = await delivery.ShipAsync(
                package: "BOX",
                contents: $"{count} 3D Model/s, each wrapped in a box",
                parcelCount: count,
                totalWeight: weight,
                ct: ct
            ).ConfigureAwait(false);
            price += shipment.Price;
        }

        price += cart.Total;
        string message = await payment.InitializePayment(
            paymentMethodId: req.PaymentMethodId,
            price: price,
            description: $"{buyer} bought {count} products for a total of {price}$."
        ).ConfigureAwait(false);

        AddProductPurchaseCommand productCommand = new(
            Ids: [.. cart.Items.Select(i => i.ProductId)]
        );
        await sender.SendCommandAsync(productCommand, ct).ConfigureAwait(false);

        return message;
    }
}
