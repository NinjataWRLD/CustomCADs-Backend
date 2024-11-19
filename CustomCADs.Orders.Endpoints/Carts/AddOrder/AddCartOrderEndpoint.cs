using CustomCADs.Orders.Application.Carts.Commands.AddOrder;
using CustomCADs.Orders.Application.Carts.Queries.IsBuyer;
using CustomCADs.Orders.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;
using CustomCADs.Shared.Queries.Products;

namespace CustomCADs.Orders.Endpoints.Carts.AddOrder;

using static ApiMessages;

public class AddCartOrderEndpoint(IRequestSender sender)
    : Endpoint<AddCartOrderRequest>
{
    public override void Configure()
    {
        Put("addOrder");
        Group<CartsGroup>();
        Description(d => d.WithSummary("2. I want to add an Order to my Cart."));
    }

    public override async Task HandleAsync(AddCartOrderRequest req, CancellationToken ct)
    {
        CartId id = new(req.CartId);
        IsCartBuyerQuery query = new(id, User.GetAccountId());

        bool userIsBuyer = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!userIsBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductPriceByIdQuery productQuery = new(new(req.ProductId));
        decimal price = await sender.SendQueryAsync(productQuery, ct).ConfigureAwait(false);

        AddCartOrderCommand commnad = new(
            Id: id,
            DeliveryType: req.DeliveryType,
            Price: new(price, "BGN"),
            Quantity: req.Quantity,
            ProductId: new(req.ProductId)
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
