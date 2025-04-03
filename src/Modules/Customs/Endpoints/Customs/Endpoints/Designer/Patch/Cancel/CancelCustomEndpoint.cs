using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Cancel;

public sealed class CancelCustomEndpoint(IRequestSender sender)
    : Endpoint<CancelCustomRequest>
{
    public override void Configure()
    {
        Patch("cancel");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Cancel")
            .WithDescription("Set an Custom's Status back to Pending")
        );
    }

    public override async Task HandleAsync(CancelCustomRequest req, CancellationToken ct)
    {
        CancelCustomCommand command = new(
            Id: CustomId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
