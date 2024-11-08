using CustomCADs.Catalog.Application.Products.Commands.Delete;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Domain.DomainEvents.Products;
using CustomCADs.Shared.Core.Events;

namespace CustomCADs.Catalog.Endpoints.Products.DeleteProduct;

using static ApiMessages;

public class DeleteProductEndpoint(IMediator mediator, IEventRaiser raiser) : Endpoint<DeleteProductRequest>
{
    public override void Configure()
    {
        Delete("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await mediator.Send(isCreatorQuery, ct).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        DeleteProductCommand command = new(req.Id);
        await mediator.Send(command, ct).ConfigureAwait(false);

        ProductDeletedEvent pdEvent = new(req.Id);
        await raiser.PublishAsync(pdEvent).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
