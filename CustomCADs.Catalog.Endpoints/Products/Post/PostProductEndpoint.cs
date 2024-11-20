using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Endpoints.Products.Get.Single;

namespace CustomCADs.Catalog.Endpoints.Products.Post;

using static Constants.Roles;

public class PostProductEndpoint(IRequestSender sender)
    : Endpoint<PostProductRequest, PostProductResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ProductsGroup>();
        Description(d => d.WithSummary("2. I want to create a Product"));
    }

    public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
    {
        CreateProductCommand command = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price, "BGN", 2, "лв"),
            ImagePath: req.ImagePath,
            CreatorId: User.GetAccountId(),
            CadPath: req.CadPath,
            Status: User.IsInRole(Designer)
                ? ProductStatus.Validated
                : ProductStatus.Unchecked
        );
        ProductId id = await sender.SendCommandAsync(command, ct);

        GetProductByIdQuery query = new(id);
        var dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostProductResponse response = dto.ToPostProductResponse();
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
