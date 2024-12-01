﻿using CustomCADs.Inventory.Application.Products.Commands.Edit;
using CustomCADs.Inventory.Application.Products.Commands.SetKeys;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.ValueObjects;

namespace CustomCADs.Inventory.Endpoints.Products.Put;

public class PutProductEndpoint(IRequestSender sender)
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
            Price: new Money(req.Price, "BGN", 2, "лв"),
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
