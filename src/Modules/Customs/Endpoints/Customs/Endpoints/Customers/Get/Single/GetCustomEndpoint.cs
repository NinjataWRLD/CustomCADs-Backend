using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;

public sealed class GetCustomEndpoint(IRequestSender sender)
	: Endpoint<GetCustomRequest, GetCustomResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Single")
			.WithDescription("See your Custom")
		);
	}

	public override async Task HandleAsync(GetCustomRequest req, CancellationToken ct)
	{
		var custom = await sender.SendQueryAsync(
			new CustomerGetCustomByIdQuery(
				Id: CustomId.New(req.Id),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetCustomResponse response = custom.ToResponse();
		await SendOkAsync(response).ConfigureAwait(false);
	}
}
