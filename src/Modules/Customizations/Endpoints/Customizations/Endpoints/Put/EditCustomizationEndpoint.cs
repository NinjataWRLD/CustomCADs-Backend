using CustomCADs.Customizations.Application.Customizations.Commands.Internal.Edit;
using CustomCADs.Customizations.Endpoints.Customizations.Endpoints;

namespace CustomCADs.Customizations.Endpoints.Customizations.Endpoints.Put;

public class EditCustomizationEndpoint(IRequestSender sender)
    : Endpoint<EditCustomizationRequest>
{
    public override void Configure()
    {
        Put("");
        Group<CustomizationsGroup>();
        Description(d => d
            .WithSummary("Edit")
            .WithDescription("Edit your Customization")
        );
    }

    public override async Task HandleAsync(EditCustomizationRequest req, CancellationToken ct)
    {
        EditCustomizationCommand command = new(
            Id: CustomizationId.New(req.Id),
            Scale: req.Scale,
            Infill: req.Infill,
            Volume: req.Volume,
            Color: req.Color,
            MaterialId: MaterialId.New(req.MaterialId)
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
