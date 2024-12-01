﻿using CustomCADs.Gallery.Application.Carts.Queries.GetById;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Single;

public class GetCartEndpoint(IRequestSender sender)
    : Endpoint<GetCartRequest, GetCartResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("05. Single")
            .WithDescription("See your Cart by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetCartRequest req, CancellationToken ct)
    {
        GetCartByIdQuery query = new(
            Id: new CartId(req.Id),
            BuyerId: User.GetAccountId()
        );
        GetCartByIdDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCartResponse response = cart.ToGetCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
