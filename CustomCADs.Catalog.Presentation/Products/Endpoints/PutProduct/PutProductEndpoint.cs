using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PutProduct;

using static Helpers.ApiMessages;

public class PutProductEndpoint(IMediator mediator) : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PutProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetId());
        bool userIsCreator = await mediator.Send(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new()
            {
                ErrorMessage = ForbiddenAccess,
            });
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery query = new(req.Id);
        GetProductByIdDto product = await mediator.Send(query, ct).ConfigureAwait(false);

        if (req.Image != null)
        {
            // Upload new image
            // Save its path
        }

        EditProductDto dto = new()
        {
            Name = req.Name,
            Description = req.Description,
            CategoryId = req.CategoryId,
            Cost = req.Cost,
        };
        EditProductCommand editCommand = new(req.Id, dto);
        await mediator.Send(editCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
