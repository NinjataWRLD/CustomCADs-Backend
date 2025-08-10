using CustomCADs.Printing.Application.Customizations.Queries.Internal.GetById;

namespace CustomCADs.Printing.Endpoints.Customizations.Endpoints.Get;

public class GetCustomizationEndpoint(IRequestSender sender)
	: Endpoint<GetCustomizationRequest, CustomizationResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CustomizationsGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("Get a Customization")
		);
	}

	public override async Task HandleAsync(GetCustomizationRequest req, CancellationToken ct)
	{
		CustomizationDto customization = await sender.SendQueryAsync(
			new GetCustomizationByIdQuery(
				Id: CustomizationId.New(req.Id)
			),
			ct
		).ConfigureAwait(false);

		CustomizationResponse response = customization.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
