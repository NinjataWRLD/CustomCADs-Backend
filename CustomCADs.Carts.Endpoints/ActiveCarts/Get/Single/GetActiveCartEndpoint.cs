using CustomCADs.Carts.Application.ActiveCarts.Queries.GetById;

namespace CustomCADs.Carts.Endpoints.ActiveCarts.Get.Single;

public sealed class GetActiveCartEndpoint(IRequestSender sender)
    : Endpoint<GetActiveCartRequest, GetActiveCartResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ActiveCartsGroup>();
        Description(d => d
            .WithSummary("04. Single")
            .WithDescription("See your Cart by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetActiveCartRequest req, CancellationToken ct)
    {
        GetActiveCartQuery query = new(
            BuyerId: User.GetAccountId()
        );
        GetActiveCartDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetActiveCartResponse response = cart.ToGetCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
