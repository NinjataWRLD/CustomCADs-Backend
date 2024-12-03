using CustomCADs.Gallery.Application.Carts.Commands.Create;
using CustomCADs.Shared.Core.Common.TypedIds.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Post.Cart;

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

        PostCartResponse response = new(id.Value);
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
