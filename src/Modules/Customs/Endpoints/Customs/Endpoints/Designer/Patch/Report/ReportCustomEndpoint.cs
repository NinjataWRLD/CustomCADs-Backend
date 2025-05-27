using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Report;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Report;

public sealed class ReportCustomEndpoint(IRequestSender sender)
	: Endpoint<ReportCustomRequest>
{
	public override void Configure()
	{
		Patch("report");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Report")
			.WithDescription("Set an Custom's Status to Reported")
		);
	}

	public override async Task HandleAsync(ReportCustomRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new ReportCustomCommand(
				Id: CustomId.New(req.Id),
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendNoContentAsync().ConfigureAwait(false);
	}
}
