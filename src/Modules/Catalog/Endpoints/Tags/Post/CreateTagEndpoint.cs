﻿using CustomCADs.Catalog.Application.Tags.Commands.Create;
using CustomCADs.Catalog.Application.Tags.Queries.GetById;

namespace CustomCADs.Catalog.Endpoints.Tags.Post;

public class CreateTagEndpoint(IRequestSender sender)
    : Endpoint<CreateTagRequest, CreateTagResponse>
{
    public override void Configure()
    {
        Post("");
        Group<TagGroup>();
        Description(d => d
            .WithSummary("01. Create")
            .WithDescription("Create Tag")
        );
    }

    public override async Task HandleAsync(CreateTagRequest req, CancellationToken ct)
    {
        CreateTagCommand command = new(
            Name: req.Name
        );
        TagId id = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        GetTagByIdQuery query = new(Id: id);
        GetTagByIdDto tag = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        CreateTagResponse response = tag.ToCreateTagResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
