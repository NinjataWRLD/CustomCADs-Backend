using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Application.Products.Queries.GetById;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Presentation.Extensions;
using CustomCADs.Catalog.Presentation.Products.Endpoints.GetProduct;
using FastEndpoints;
using Mapster;
using Wolverine;

namespace CustomCADs.Catalog.Presentation.Products.Endpoints.PostProduct;

public class PostProductEndpoint(IMessageBus bus) : Endpoint<PostProductRequest, PostProductResponse>
{
    public override void Configure()
    {
        Post("");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PostProductRequest>("multipart/form-data"));
    }

    public override async Task HandleAsync(PostProductRequest req, CancellationToken ct)
    {
        CreateProductDto dto = new()
        {
            Name = req.Name,
            Description = req.Description,
            CategoryId = req.CategoryId,
            Cost = req.Cost,
            CreatorId = User.GetId(),
            Status = User.IsInRole("Designer") ? ProductStatus.Validated : ProductStatus.Unchecked, // Role Constants
        };
        CreateProductCommand command = new(dto);
        var id = await bus.InvokeAsync<Guid>(command, ct).ConfigureAwait(false);

        // Upload image
        // Upload file
        // Save Paths

        GetProductByIdQuery query = new(id);
        var product = await bus.InvokeAsync<GetProductByIdDto>(query, ct).ConfigureAwait(false);

        var response = product.Adapt<PostProductResponse>();
        await SendCreatedAtAsync<GetProductEndpoint>(new { id }, response).ConfigureAwait(false);
    }
}
