using CustomCADs.Catalog.Application.Tags.Commands.Internal.Delete;

namespace CustomCADs.Catalog.Endpoints.Tags.Endpoints.Delete;

public class DeleteTagEndpoint(IRequestSender sender)
    : Endpoint<DeleteTagRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<TagGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete Tag")
        );
    }

    public override async Task HandleAsync(DeleteTagRequest req, CancellationToken ct)
    {
        await sender.SendCommandAsync(
            new DeleteTagCommand(
                Id: TagId.New(req.Id)
            ), 
            ct
        ).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
