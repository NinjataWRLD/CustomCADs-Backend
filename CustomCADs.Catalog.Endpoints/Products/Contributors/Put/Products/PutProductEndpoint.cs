using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Commands.SetKeys;
using CustomCADs.Catalog.Endpoints.Products.Contributors;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Contributors.Put.Products;

public sealed class PutProductEndpoint(IRequestSender sender)
    : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Description(d => d
            .WithSummary("05. Edit")
            .WithDescription("Edit your Product by specifying its Id and a new Name, Description, CategoryId, Price and ImageKey")
        );
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        EditProductCommand editCommand = new(
            Id: new ProductId(req.Id),
            Name: req.Name,
            Description: req.Description,
            CategoryId: new CategoryId(req.CategoryId),
            Price: req.Price,
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(editCommand, ct).ConfigureAwait(false);

        if (req.ImageKey is not null)
        {
            SetProductKeysCommand keysCommand = new(
                Id: new ProductId(req.Id),
                CadKey: null,
                ImageKey: req.ImageKey,
                CreatorId: User.GetAccountId()
            );
            await sender.SendCommandAsync(keysCommand, ct).ConfigureAwait(false);
        }

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
