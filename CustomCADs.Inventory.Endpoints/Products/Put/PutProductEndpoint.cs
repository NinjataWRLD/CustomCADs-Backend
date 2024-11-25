using CustomCADs.Inventory.Application.Products.Commands.Edit;
using CustomCADs.Inventory.Application.Products.Commands.SetKeys;
using CustomCADs.Inventory.Application.Products.Queries.IsCreator;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

namespace CustomCADs.Inventory.Endpoints.Products.Put;

using static ApiMessages;

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
        ProductId id = new(req.Id);
        IsProductCreatorQuery isCreatorQuery = new(id, User.GetAccountId());
        bool userIsCreator = await sender.SendQueryAsync(isCreatorQuery).ConfigureAwait(false);

        if (!userIsCreator)
        {
            ValidationFailures.Add(new("Id", ForbiddenAccess, id));
            await SendErrorsAsync().ConfigureAwait(false);
            return;
        }

        EditProductCommand editCcommand = new(
            Id: id,
            Name: req.Name,
            Description: req.Description,
            CategoryId: new(req.CategoryId),
            Price: new(req.Price, "BGN", 2, "лв")
        );
        await sender.SendCommandAsync(editCcommand, ct).ConfigureAwait(false);

        if (req.ImageKey is not null)
        {
            SetProductKeysCommand keysCommand = new(
                id,
                CadKey: null,
                ImageKey: req.ImageKey
            );
            await sender.SendCommandAsync(keysCommand, ct).ConfigureAwait(false);
        }

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
