using CustomCADs.Catalog.Application.Products.Commands.Delete;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Events.Products;
using FastEndpoints;
using MediatR;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Products.DeleteProduct;

using static Helpers.ApiMessages;

public class DeleteProductEndpoint(IMediator mediator, IMessageBus bus) : Endpoint<DeleteProductRequest>
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
        await bus.PublishAsync(pdEvent).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
