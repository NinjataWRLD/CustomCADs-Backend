using CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.Normal;
using CustomCADs.Shared.Core.Common.TypedIds.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Client.Post.Purchase.Normal;

public sealed class PurchaseCustomEndpoint(IRequestSender sender)
    : Endpoint<PurchaseCustomRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<ClientGroup>();
        Description(d => d
            .WithSummary("Purchase")
            .WithDescription("Purchase a Custom")
        );
    }

    public override async Task HandleAsync(PurchaseCustomRequest req, CancellationToken ct)
    {
        PurchaseCustomCommand command = new(
            Id: CustomId.New(req.Id),
            PaymentMethodId: req.PaymentMethodId,
            BuyerId: User.GetAccountId()
        );
        string message = await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendOkAsync(message).ConfigureAwait(false);
    }
}
