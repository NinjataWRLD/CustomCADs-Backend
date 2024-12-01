﻿using CustomCADs.Gallery.Application.Carts.Commands.RemoveItem;

namespace CustomCADs.Gallery.Endpoints.Carts.Delete.Item;
public class DeleteCartItemEndpoint(IRequestSender sender)
    : Endpoint<DeleteItemItemRequest>
{
    public override void Configure()
    {
        Delete("items");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("05. Remove Item")
            .WithDescription("Remove an Item from your Cart by specifying the Cart and Item's Ids")
        );
    }

    public override async Task HandleAsync(DeleteItemItemRequest req, CancellationToken ct)
    {
        RemoveCartItemCommand commnad = new(
            Id: new(req.CartId),
            ItemId: new(req.ItemId),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
