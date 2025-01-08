using CustomCADs.Catalog.Application.Products.Commands.Edit;
using CustomCADs.Catalog.Application.Products.Commands.SetFiles;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Creator.Put.Products;

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
        var id = ProductId.New(req.Id);

        EditProductCommand editCommand = new(
            Id: id,
            Name: req.Name,
            Description: req.Description,
            CategoryId: CategoryId.New(req.CategoryId),
            Price: req.Price,
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(editCommand, ct).ConfigureAwait(false);

        SetProductFilesCommand keysCommand = new(
            Id: id,
            Cad: (req.CadKey, req.CadContentType),
            Image: (req.ImageKey, req.ImageContentType),
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(keysCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
