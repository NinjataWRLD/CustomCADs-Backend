using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Delete;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customers.Delete;

public sealed class DeleteCustomEndpoint(IRequestSender sender)
	: Endpoint<DeleteCustomRequest>
{
	public override void Configure()
	{
		Delete("");
		Group<CustomerGroup>();
		Description(d => d
			.WithSummary("Delete")
			.WithDescription("Delete your Custom")
		);
	}

	public override async Task HandleAsync(DeleteCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new DeleteCustomCommand(
				Id: CustomId.New(req.Id),
				BuyerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
