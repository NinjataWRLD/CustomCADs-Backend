using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Client.Get.Single;

public sealed class GetCompletedOrderEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrderRequest, GetCompletedOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("05. Single")
            .WithDescription("See your Completed Order by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetCompletedOrderRequest req, CancellationToken ct)
    {
        ClientGetCompletedOrderByIdQuery query = new(
            Id: CompletedOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        ClientGetCompletedOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToGetCompletedOrderResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
