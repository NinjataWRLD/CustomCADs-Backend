using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Designer.Patch.Report;

public sealed class ReportProductEndpoint(IRequestSender sender)
	: Endpoint<ReportProductRequest>
{
	public override void Configure()
	{
		Patch("report");
		Group<DesignerGroup>();
		Description(d => d
			.WithSummary("Report")
			.WithDescription("Set a Product's Status to Reported")
		);
	}

	public override async Task HandleAsync(ReportProductRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new SetProductStatusCommand(
				Id: ProductId.New(req.Id),
				Status: ProductStatus.Reported,
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
