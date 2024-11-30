using CustomCADs.Inventory.Application.Products.Queries.DesignerGetById;

namespace CustomCADs.Inventory.Endpoints.Designer.Get.Single;

public class DesignerSingleProductEndpoint(IRequestSender sender)
    : Endpoint<DesignerSingleProductRequest, DesignerSingleProductResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to see an Unchecked Product in detail"));
    }

    public override async Task HandleAsync(DesignerSingleProductRequest req, CancellationToken ct)
    {
        DesignerGetProductByIdQuery query = new(
            Id: new(req.Id), 
            DesignerId: User.GetAccountId()
        );
        DesignerGetProductByIdDto product = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        DesignerSingleProductResponse response = product.ToDesignerSingleProductResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
