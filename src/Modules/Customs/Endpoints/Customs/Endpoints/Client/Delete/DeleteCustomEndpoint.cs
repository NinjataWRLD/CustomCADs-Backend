using CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Delete;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Delete;

public sealed class DeleteCustomEndpoint(IRequestSender sender)
    : Endpoint<DeleteCustomRequest>
{
    public override void Configure()
    {
        Delete("");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Delete")
            .WithDescription("Delete your Custom")
        );
    }

    public override async Task HandleAsync(DeleteCustomRequest req, CancellationToken ct)
    {
        DeleteCustomCommand command = new(
            Id: CustomId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
