using CustomCADs.Inventory.Application.Products.Commands.Edit;
using CustomCADs.Inventory.Application.Products.Commands.SetKeys;

namespace CustomCADs.Inventory.Endpoints.Products.Put;

public class PutProductEndpoint(IRequestSender sender)
    : Endpoint<PutProductRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<ProductsGroup>();
        Options(o => o.Accepts<PutProductRequest>("multipart/form-data"));
        Description(d => d.WithSummary("6. I want to edit my Product"));
    }

    public override async Task HandleAsync(PutProductRequest req, CancellationToken ct)
    {
        EditProductCommand editCcommand = new(
            Id: new(req.Id),
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price, "BGN", 2, "лв"),
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(editCcommand, ct).ConfigureAwait(false);

        if (req.ImageKey is not null)
        {
            SetProductKeysCommand keysCommand = new(
                Id: new(req.Id),
                CadKey: null,
                ImageKey: req.ImageKey,
                CreatorId: User.GetAccountId()
            );
            await sender.SendCommandAsync(keysCommand, ct).ConfigureAwait(false);
        }

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
