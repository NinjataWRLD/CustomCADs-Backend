using CustomCADs.Orders.Application.CompletedOrders.Queries.Internal.ClientGetById;

namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client.Get.Single;

public sealed class GetCompletedOrderEndpoint(IRequestSender sender)
    : Endpoint<GetCompletedOrderRequest, GetCompletedOrderResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See your Completed Order")
        );
    }

    public override async Task HandleAsync(GetCompletedOrderRequest req, CancellationToken ct)
    {
        ClientGetCompletedOrderByIdQuery query = new(
            Id: CompletedOrderId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        ClientGetCompletedOrderByIdDto order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
