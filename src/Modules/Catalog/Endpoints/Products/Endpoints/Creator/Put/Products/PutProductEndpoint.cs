using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetFiles;
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

        await sender.SendCommandAsync(new EditProductCommand(
                Id: id,
                Name: req.Name,
                Description: req.Description,
                CategoryId: CategoryId.New(req.CategoryId),
                Price: req.Price,
                CreatorId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await sender.SendCommandAsync(
            new SetProductFilesCommand(
                Id: id,
                Cad: (req.CadKey, req.CadContentType, req.CadVolume),
                Image: (req.ImageKey, req.ImageContentType),
                CreatorId: User.GetAccountId()
            ),
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
