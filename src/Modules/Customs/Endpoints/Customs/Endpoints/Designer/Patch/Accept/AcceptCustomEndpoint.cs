using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Accept;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Accept;

public sealed class AcceptCustomEndpoint(IRequestSender sender)
	: Endpoint<AcceptCustomRequest>
{
	public override void Configure()
	{
		Patch("accept");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Accept")
			.WithDescription("Set an Custom's Status to Accepted")
		);
	}

	public override async Task HandleAsync(AcceptCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new AcceptCustomCommand(
				Id: CustomId.New(req.Id),
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
