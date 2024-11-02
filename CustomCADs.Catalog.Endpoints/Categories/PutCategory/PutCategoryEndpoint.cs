﻿using CustomCADs.Catalog.Application.Categories.Commands;
using CustomCADs.Catalog.Application.Categories.Commands.Edit;
using FastEndpoints;
using Wolverine;

namespace CustomCADs.Catalog.Endpoints.Categories.PutCategory;

public class PutCategoryEndpoint(IMessageBus bus) : Endpoint<PutCategoryRequest>
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
        await bus.InvokeAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}