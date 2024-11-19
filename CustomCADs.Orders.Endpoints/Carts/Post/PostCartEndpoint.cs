using CustomCADs.Orders.Application.Carts.Commands.Create;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Orders;

namespace CustomCADs.Orders.Endpoints.Carts.Post;

public class PostCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<PostCartResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CartsGroup>();
        Description(d => d.WithSummary("1. I want to create a Cart"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CreateCartCommand command = new(User.GetAccountId());
        CartId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        PostCartResponse response = new(id.Value);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
