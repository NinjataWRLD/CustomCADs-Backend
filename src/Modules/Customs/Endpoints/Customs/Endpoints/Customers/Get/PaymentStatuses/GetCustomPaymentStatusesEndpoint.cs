using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Get.PaymentStatuses;

public sealed class GetCustomPaymentStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<string[]>
{
	public override void Configure()
	{
		Get("payment-statuses");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Payment Statuses")
			.WithDescription("See all Custom Payment Status types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		string[] result = await sender.SendQueryAsync(
			new GetCustomPaymentStatusesQuery(),
			ct
		).ConfigureAwait(false);

		await SendOkAsync(result).ConfigureAwait(false);
	}
}
