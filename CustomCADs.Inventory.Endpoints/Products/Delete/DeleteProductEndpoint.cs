using CustomCADs.Inventory.Application.Products.Commands.Delete;
using CustomCADs.Inventory.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Inventory;

namespace CustomCADs.Inventory.Endpoints.Products.Delete;

using static ApiMessages;

public class DeleteProductEndpoint(IRequestSender sender)
    : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("8. I want to delete my Product"));
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        ProductId id = new(req.Id);
        IsProductCreatorQuery isCreatorQuery = new(id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery, ct).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteProductCommand command = new(id);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
