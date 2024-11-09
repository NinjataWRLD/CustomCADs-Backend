﻿using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Edit;

namespace CustomCADs.Catalog.Endpoints.Categories.PutCategory;

public class PutCategoryEndpoint(IRequestSender sender)
    : Endpoint<PutCategoryRequest>
{
    public override void Configure()
    {
        Put("{id}");
        Group<CategoriesGroup>();
    }

    public override async Task HandleAsync(PutCategoryRequest req, CancellationToken ct)
    {
        CategoryWriteDto category = new(req.Name);
        EditCategoryCommand command = new(req.Id, category);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
