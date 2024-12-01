using CustomCADs.Inventory.Application.Products.Queries.GetById;

namespace CustomCADs.Inventory.Endpoints.Products.Get.Single;

public class GetProductEndpoint(IRequestSender sender)
    : Endpoint<GetProductRequest, GetProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("06. Single")
            .WithDescription("See your Product by specifying its Id")
        );
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        GetProductByIdQuery getProductQuery = new(
            Id: new ProductId(req.Id),
            CreatorId: User.GetAccountId()
        );
        GetProductByIdDto product = await sender.SendQueryAsync(getProductQuery, ct).ConfigureAwait(false);

        GetProductResponse response = product.ToGetProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
