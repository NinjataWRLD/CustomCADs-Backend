using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Finish;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Finish;

public sealed class FinishCustomEndpoint(IRequestSender sender)
    : Endpoint<FinishCustomRequest>
{
    public override void Configure()
    {
        Patch("finish");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Finish")
            .WithDescription("Set an Custom's Status to Finished")
        );
    }

    public override async Task HandleAsync(FinishCustomRequest req, CancellationToken ct)
    {
        FinishCustomCommand command = new(
            Id: CustomId.New(req.Id),
            Price: req.Price,
            Cad: req.ToTuple(),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
