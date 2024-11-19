using CustomCADs.Gallery.Application.Carts.Commands.Delete;
using CustomCADs.Gallery.Application.Carts.Queries.IsBuyer;
using CustomCADs.Gallery.Endpoints.Helpers;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Gallery;

namespace CustomCADs.Gallery.Endpoints.Carts.Delete;

using static ApiMessages;

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
        CartId id = new(req.Id);
        IsCartBuyerQuery query = new(id, User.GetAccountId());

        bool userIsBuyer = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);
        if (!userIsBuyer)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteCartCommand commnad = new(id);
        await sender.SendCommandAsync(commnad, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
