using CustomCADs.Inventory.Application.Products.Commands.SetStatus;
using CustomCADs.Shared.Core.Common.TypedIds.Inventory;

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
        ProductId id = new(req.Id);
        SetProductStatusCommand command = new(id, req.Status);
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
