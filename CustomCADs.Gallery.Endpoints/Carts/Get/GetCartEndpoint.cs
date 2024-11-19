using CustomCADs.Gallery.Application.Carts.Queries.GetById;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Get;

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
        CartId id = new(req.Id);
        GetCartByIdQuery query = new(id);
        GetCartByIdDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCartResponse response = cart.ToGetCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
