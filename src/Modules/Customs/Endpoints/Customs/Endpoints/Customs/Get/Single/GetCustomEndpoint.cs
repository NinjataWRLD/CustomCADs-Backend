using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Customs.Get.Single;

public sealed class GetCustomEndpoint(IRequestSender sender)
    : Endpoint<GetCustomRequest, GetCustomResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<CustomerGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See your Custom")
        );
    }

    public override async Task HandleAsync(GetCustomRequest req, CancellationToken ct)
    {
        ClientGetCustomByIdQuery query = new(
            Id: CustomId.New(req.Id),
            BuyerId: User.GetAccountId()
        );
        var order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        GetCustomResponse response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
