﻿using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.ClientGetById;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Endpoints.Client.Get.Single;

public sealed class GetOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<GetOngoingOrderRequest, GetOngoingOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See your Order")
        );
    }

    public override async Task HandleAsync(GetOngoingOrderRequest req, CancellationToken ct)
    {
        ClientGetOngoingOrderByIdQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        ClientGetOngoingOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOngoingOrderResponse response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
