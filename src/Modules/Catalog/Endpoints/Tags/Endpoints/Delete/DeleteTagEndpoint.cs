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
        DeleteTagCommand command = new(
            Id: TagId.New(req.Id)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
