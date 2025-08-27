using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Shared.Endpoints.Extensions;

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
		CustomerGetCustomByIdDto custom = await sender.SendQueryAsync(
			new CustomerGetCustomByIdQuery(
				Id: CustomId.New(req.Id),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		GetCustomResponse response = custom.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
