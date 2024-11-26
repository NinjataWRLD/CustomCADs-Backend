using CustomCADs.Inventory.Application.Products.Commands.SetStatus;

namespace CustomCADs.Inventory.Endpoints.Designer.Patch;

public class PatchProductStatusEndpoint(IRequestSender sender)
    : Endpoint<PatchProductStatusRequest>
{
    public override void Configure()
    {
        Patch("{id}/status");
        Group<DesignerGroup>();
        Description(d => d.WithSummary("I want to change the Status of a Product"));
    }

    public override async Task HandleAsync(PatchProductStatusRequest req, CancellationToken ct)
    {
        SetProductStatusCommand command = new(
            Id: new(req.Id),
            Status: req.Status,
            CreatorId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
