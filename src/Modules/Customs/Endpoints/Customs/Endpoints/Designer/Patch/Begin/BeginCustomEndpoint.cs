using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.Begin;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Patch.Begin;

public sealed class BeginCustomEndpoint(IRequestSender sender)
    : Endpoint<BeginCustomRequest>
{
    public override void Configure()
    {
        Patch("begin");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Begin")
            .WithDescription("Set an Custom's Status to Begun")
        );
    }

    public override async Task HandleAsync(BeginCustomRequest req, CancellationToken ct)
    {
        BeginCustomCommand command = new(
            Id: CustomId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
