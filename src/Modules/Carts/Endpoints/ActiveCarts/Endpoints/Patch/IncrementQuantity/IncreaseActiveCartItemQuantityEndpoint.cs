using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Quantity.Increment;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints.Patch.IncrementQuantity;

public class IncreaseActiveCartItemQuantityEndpoint(IRequestSender sender)
    : Endpoint<IncreaseActiveCartItemQuantityRequest>
{
    public override void Configure()
    {
        Patch("increase");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("Increase")
            .WithDescription("Increase the Cart Item's quantity")
        );
    }

    public override async Task HandleAsync(IncreaseActiveCartItemQuantityRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new IncreaseActiveCartItemQuantityCommand(
                BuyerId: User.GetAccountId(),
                ProductId: ProductId.New(req.ProductId),
                Amount: req.Amount
            ),
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
