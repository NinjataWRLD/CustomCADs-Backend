using CustomCADs.Catalog.Application.Products.Queries.DesignerGetById;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Get.Single;

public sealed class DesignerSingleProductEndpoint(IRequestSender sender)
    : Endpoint<DesignerSingleProductRequest, DesignerSingleProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("02. Single Unchecked")
            .WithDescription("See an Unchecked Product")
        );
    }

    public override async Task HandleAsync(DesignerSingleProductRequest req, CancellationToken ct)
    {
        DesignerGetProductByIdQuery query = new(
            Id: ProductId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        DesignerGetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        DesignerSingleProductResponse response = product.ToDesignerSingleProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
