using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Cancel;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Cancel;

public sealed class CancelCustomEndpoint(IRequestSender sender)
	: Endpoint<CancelCustomRequest>
{
	public override void Configure()
	{
		Patch("cancel");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Cancel")
			.WithDescription("Set an Custom's Status back to Pending")
		);
	}

	public override async Task HandleAsync(CancelCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new CancelCustomCommand(
				Id: CustomId.New(req.Id),
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
