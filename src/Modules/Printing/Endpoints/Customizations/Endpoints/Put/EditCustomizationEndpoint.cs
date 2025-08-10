using CustomCADs.Printing.Application.Customizations.Commands.Internal.Edit;

namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints.Put;

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
		await sender.SendCommandAsync(
			new EditCustomizationCommand(
				Id: CustomizationId.New(req.Id),
				Scale: req.Scale,
				Infill: req.Infill,
				Volume: req.Volume,
				Color: req.Color,
				MaterialId: MaterialId.New(req.MaterialId)
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
