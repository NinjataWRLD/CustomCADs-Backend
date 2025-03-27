using CustomCADs.Catalog.Application.Products.Commands.Internal.Create;
using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetById;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Get.Single;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Post.Products;

public sealed class PostProductEndpoint(IRequestSender sender)
    : Endpoint<PostProductRequest, PostProductResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Create")
            .WithDescription("Create a Product")
        );
    }

    public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
    {
        CreateProductCommand command = new(
            Name: req.Name,
            Description: req.Description,
            CategoryId: CategoryId.New(req.CategoryId),
            Price: req.Price,
            ImageKey: req.ImageKey,
            ImageContentType: req.ImageContentType,
            CadKey: req.CadKey,
            CadContentType: req.CadContentType,
            CadVolume: req.CadVolume,
            CreatorId: User.GetAccountId()
        );
        ProductId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        CreatorGetProductByIdQuery query = new(
            Id: id,
            CreatorId: User.GetAccountId()
        );
        CreatorGetProductByIdDto dto = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        PostProductResponse response = dto.ToPostResponse();
        await SendCreatedAtAsync<GetProductEndpoint>(new { Id = id.Value }, response).ConfigureAwait(false);
    }
}
