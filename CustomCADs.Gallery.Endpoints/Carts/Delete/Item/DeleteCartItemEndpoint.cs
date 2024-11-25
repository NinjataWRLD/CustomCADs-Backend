using CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;
using CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;
using CustomCADs.Gallery.Endpoints.Helpers;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Delete.Item;

using static ApiMessages;

public class DeleteCartItemEndpoint(IRequestSender sender)
    : Endpoint<DeleteItemItemRequest>
{
    public override void Configure()
    {
        Delete("items");
        Group<CartsGroup>();
        Description(d => d.WithSummary("4. I want to remove an Item from my Cart."));
    }

    public override async Task HandleAsync(DeleteItemItemRequest req, CancellationToken ct)
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

        RemoveCartItemCommand commnad = new(
            Id: id,
            ItemId: new(req.ItemId)
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
