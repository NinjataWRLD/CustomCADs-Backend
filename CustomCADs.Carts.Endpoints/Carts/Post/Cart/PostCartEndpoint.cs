using CustomCADs.Carts.Application.Carts.Commands.Create;
using CustomCADs.Carts.Application.Carts.Queries.GetById;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;

namespace CustomCADs.Carts.Endpoints.Carts.Post.Cart;

public sealed class PostCartEndpoint(IRequestSender sender)
    : EndpointWithoutRequest<PostCartResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CartsGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Create a Cart")
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        CreateCartCommand command = new(User.GetAccountId());
        CartId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetCartByIdQuery query = new(id, User.GetAccountId());
        GetCartByIdDto cart = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = cart.ToPostCartResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
