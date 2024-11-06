using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core;
using FastEndpoints;
using MediatR;
using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.GetProduct;

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
        IsProductCreatorQuery isCreatorQuery = new(req.Id, User.GetAccountId());
        bool userIsCreator = await mediator.Send(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, req.Id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(req.Id);
        GetProductByIdDto product = await mediator.Send(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = new(
            Id: product.Id,
            Name: product.Name,
            Cost: product.Cost,
            Description: product.Description,
            UploadDate: product.UploadDate.ToString(DateFormatString),
            CamCoordinates: new(product.Cad.CamCoordinates.X, product.Cad.CamCoordinates.Y, product.Cad.CamCoordinates.Z),
            PanCoordinates: new(product.Cad.PanCoordinates.X, product.Cad.PanCoordinates.Y, product.Cad.PanCoordinates.Z),
            CadPath: product.Cad.Path,
            Category: new() { Id = product.Category.Id, Name = product.Category.Name }
        );
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
