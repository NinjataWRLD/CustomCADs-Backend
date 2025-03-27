using CustomCADs.Catalog.Application.Products.Commands.Internal.Edit;
using CustomCADs.Catalog.Application.Products.Commands.Internal.SetFiles;
using CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Creator.Put.Products;

public sealed class PutProductEndpoint(IRequestSender sender)
    : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("");
        Group<CreatorGroup>();
        Description(d => d
            .WithSummary("Edit")
            .WithDescription("Edit your Product")
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

        SetProductFilesCommand filesCommand = new(
            Id: id,
            Cad: (req.CadKey, req.CadContentType, req.CadVolume),
            Image: (req.ImageKey, req.ImageContentType),
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(filesCommand, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
