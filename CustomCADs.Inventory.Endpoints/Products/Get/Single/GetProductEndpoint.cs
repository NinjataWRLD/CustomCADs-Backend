using CustomCADs.Inventory.Application.Products.Queries.GetById;
using CustomCADs.Inventory.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Single;

using static ApiMessages;

public class GetProductEndpoint(IRequestSender sender)
    : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("7. I want to see my Product in detail"));
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        ProductId id = new(req.Id);
        IsProductCreatorQuery isCreatorQuery = new(id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        GetProductByIdQuery getProductQuery = new(id);
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = product.ToGetProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
