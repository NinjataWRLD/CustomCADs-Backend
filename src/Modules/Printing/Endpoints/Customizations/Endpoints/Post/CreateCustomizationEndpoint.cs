using CustomCADs.Printing.Application.Customizations.Commands.Internal.Create;
using CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;
using CustomCADs.Printing.Endpoints.Customizations.Endpoints.Get;

namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints.Post;

public class CreateCustomizationEndpoint(IRequestSender sender)
	: Endpoint<CreateCustomizationRequest, CustomizationResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CustomizationsGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Create a Customization")
		);
	}

	public override async Task HandleAsync(CreateCustomizationRequest req, CancellationToken ct)
	{
		CustomizationId customizationId = await sender.SendCommandAsync(
			new CreateCustomizationCommand(
				Scale: req.Scale,
				Infill: req.Infill,
				Volume: req.Volume,
				Color: req.Color,
				MaterialId: MaterialId.New(req.MaterialId)
			),
			ct
		).ConfigureAwait(false);

		CustomizationDto customization = await sender.SendQueryAsync(
			new GetCustomizationByIdQuery(customizationId)
		, ct).ConfigureAwait(false);

		CustomizationResponse response = customization.ToResponse();
		await SendCreatedAtAsync<GetCustomizationEndpoint>(new { id = customizationId.Value }, response).ConfigureAwait(false);
	}
}
