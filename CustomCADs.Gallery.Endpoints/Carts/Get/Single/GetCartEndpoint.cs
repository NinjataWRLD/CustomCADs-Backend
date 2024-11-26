using CustomCADs.Gallery.Application.Carts.Queries.GetById;

namespace CustomCADs.Gallery.Endpoints.Carts.Get.Single;

public class GetCartEndpoint(IRequestSender sender)
    : Endpoint<GetCartRequest, GetCartResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<CartsGroup>();
        Description(d => d.WithSummary("5. I want to see my Cart in detail"));
    }

    public override async Task HandleAsync(GetCartRequest req, CancellationToken ct)
    {
        GetCartByIdQuery query = new(
            Id: new(req.Id),
            BuyerId: User.GetAccountId()
        );
        GetCartByIdDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCartResponse response = cart.ToGetCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
