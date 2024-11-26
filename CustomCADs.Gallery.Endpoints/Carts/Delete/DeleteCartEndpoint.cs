using CustomCADs.Gallery.Application.Carts.Commands.Delete;

namespace CustomCADs.Gallery.Endpoints.Carts.Delete;
public class DeleteCartEndpoint(IRequestSender sender)
    : Endpoint<DeleteCartRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<CartsGroup>();
        Description(d => d.WithSummary("6. I want to delete my Cart"));
    }

    public override async Task HandleAsync(DeleteCartRequest req, CancellationToken ct)
    {
        DeleteCartCommand command = new(
            Id: new(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
