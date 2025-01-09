using CustomCADs.Orders.Application.OngoingOrders.Queries.ClientGetById;

namespace CustomCADs.Orders.Endpoints.OngoingOrders.Client.Get.Single;

public sealed class GetOngoingOrderEndpoint(IRequestSender sender)
    : Endpoint<GetOngoingOrderRequest, GetOngoingOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("05. Single")
            .WithDescription("See your Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetOngoingOrderRequest req, CancellationToken ct)
    {
        ClientGetOngoingOrderByIdQuery query = new(
            Id: OngoingOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        ClientGetOngoingOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetOngoingOrderResponse response = order.ToGetOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
