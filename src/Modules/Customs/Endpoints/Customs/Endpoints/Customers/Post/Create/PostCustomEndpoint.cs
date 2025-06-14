using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.Single;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Post.Create;

public sealed class PostCustomEndpoint(IRequestSender sender)
	: Endpoint<PostCustomRequest, PostCustomResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Create")
			.WithDescription("Request a Custom")
		);
	}

	public override async Task HandleAsync(PostCustomRequest req, CancellationToken ct)
	{
		CustomId id = await sender.SendCommandAsync(
			new CreateCustomCommand(
				Name: req.Name,
				Description: req.Description,
				ForDelivery: req.ForDelivery,
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		var custom = await sender.SendQueryAsync(
			new CustomerGetCustomByIdQuery(
				Id: id,
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		PostCustomResponse response = custom.ToPostResponse();
		await SendCreatedAtAsync<GetCustomEndpoint>(
			routeValues: new { Id = id.Value },
			responseBody: response
		).ConfigureAwait(false);
	}
}
