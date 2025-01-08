using CustomCADs.Catalog.Application.Products.Commands.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;

namespace CustomCADs.Catalog.Endpoints.Products.Designer.Patch.Report;

public sealed class ReportProductEndpoint(IRequestSender sender)
    : Endpoint<ReportProductRequest>
{
    public override void Configure()
    {
        Patch("{id}/report");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("04. Report")
            .WithDescription("Set a Product's Status to Reported")
        );
    }

    public override async Task HandleAsync(ReportProductRequest req, CancellationToken ct)
    {
        SetProductStatusCommand command = new(
            Id: ProductId.New(req.Id),
            Status: ProductStatus.Reported,
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
