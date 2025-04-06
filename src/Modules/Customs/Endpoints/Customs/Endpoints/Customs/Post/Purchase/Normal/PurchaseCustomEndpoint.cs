using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Post.Purchase.Normal;

public sealed class PurchaseCustomEndpoint(IRequestSender sender)
    : Endpoint<PurchaseCustomRequest, string>
{
    public override void Configure()
    {
        Post("purchase");
        Group<CustomerGroup>();
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
