using CustomCADs.Catalog.Application.Products.Commands.Delete;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.Products.DomainEvents;
using CustomCADs.Shared.Application.Events;

namespace CustomCADs.Catalog.Endpoints.Products.Delete;

using static ApiMessages;

public class DeleteProductEndpoint(IRequestSender sender, IEventRaiser raiser)
    : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("7. I want to delete my Product"));
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

        await raiser.RaiseAsync(new ProductDeletedDomainEvent(
            Id: id
        )).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
