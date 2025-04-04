﻿using CustomCADs.Catalog.Application.Tags.Commands.Internal.Edit;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Put;

public class EditTagEndpoint(IRequestSender sender)
    : Endpoint<EditTagRequest>
{
    public override void Configure()
    {
        Put("");
        Group<TagGroup>();
        Description(d => d
            .WithSummary("Edit")
            .WithDescription("Edit Tag")
        );
    }

    public override async Task HandleAsync(EditTagRequest req, CancellationToken ct)
    {
        EditTagCommand command = new(
            Id: TagId.New(req.Id),
            Name: req.Name
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
