﻿using CustomCADs.Orders.Application.OngoingOrders.Commands.Create;
using CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;
using CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Single;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Post.Create;

public sealed class PostOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<PostOngoingOrderRequest, PostOngoingOrderResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Make an Order")
        );
    }

    public override async Task HandleAsync(PostOngoingOrderRequest req, CancellationToken ct)
    {
        CreateOngoingOrderCommand command = new(
            Name: req.Name,
            Description: req.Description,
            Delivery: req.Delivery,
            BuyerId: User.GetAccountId()
        );
        OngoingOrderId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        ClientGetOngoingOrderByIdQuery query = new(
            Id: id,
            BuyerId: User.GetAccountId()
        );
        ClientGetOngoingOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostOngoingOrderResponse response = order.ToPostOrderResponse();
        await SendCreatedAtAsync<GetOngoingOrderEndpoint>(
            routeValues: new { Id = id.Value },
            responseBody: response
        ).ConfigureAwait(false);
    }
}
