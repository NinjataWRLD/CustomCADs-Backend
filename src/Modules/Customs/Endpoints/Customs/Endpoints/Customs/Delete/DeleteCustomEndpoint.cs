using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Delete;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Delete;

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
        DeleteCustomCommand command = new(
            Id: CustomId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        await sender.SendCommandAsync(command, ct).ConfigureAwait(false);

        await SendNoContentAsync().ConfigureAwait(false);
    }
}
