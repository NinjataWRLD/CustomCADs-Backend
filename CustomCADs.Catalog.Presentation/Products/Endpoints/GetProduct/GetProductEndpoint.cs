using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Catalog.Presentation.Extensions;
using FastEndpoints;
using Mapster;
using MediatR;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.GetProduct;

using static Helpers.ApiMessages;

public class GetProductEndpoint(IMediator mediator) : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
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

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await mediator.Send(getProductQuery, ct).ConfigureAwait(false);

        var response = product.Adapt<GetProductResponse>();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
