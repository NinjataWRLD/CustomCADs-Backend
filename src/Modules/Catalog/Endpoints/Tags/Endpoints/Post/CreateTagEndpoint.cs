using CustomCADs.Catalog.Application.Tags.Commands.Internal.Create;
using CustomCADs.Catalog.Application.Tags.Queries.Internal.GetById;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Post;

public class CreateTagEndpoint(IRequestSender sender)
    : Endpoint<CreateTagRequest, CreateTagResponse>
{
    public override void Configure()
    {
        Post("");
        Group<TagGroup>();
        Description(d => d
            .WithSummary("Create")
            .WithDescription("Create Tag")
        );
    }

    public override async Task HandleAsync(CreateTagRequest req, CancellationToken ct)
    {
        TagId id = await sender.SendCommandAsync(
            new CreateTagCommand(
                Name: req.Name
            ),
            ct
        ).ConfigureAwait(false);

        GetTagByIdDto tag = await sender.SendQueryAsync(
            new GetTagByIdQuery(id),
            ct
        ).ConfigureAwait(false);

        CreateTagResponse response = tag.ToCreateTagResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
