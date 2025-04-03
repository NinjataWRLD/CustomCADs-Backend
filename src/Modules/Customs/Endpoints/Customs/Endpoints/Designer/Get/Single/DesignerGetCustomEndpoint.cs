using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

namespace CustomCADs.Customs.Endpoints.Customs.Endpoints.Designer.Get.Single;

public sealed class DesignerGetCustomEndpoint(IRequestSender sender)
    : Endpoint<DesignerGetCustomRequest, DesignerGetCustomResponse>
{
    public override void Configure()
    {
        Get("{id}");
        Group<DesignerGroup>();
        Description(d => d
            .WithSummary("Single")
            .WithDescription("See an Accepted by You or a Pending Custom")
        );
    }

    public override async Task HandleAsync(DesignerGetCustomRequest req, CancellationToken ct)
    {
        DesignerGetCustomByIdQuery query = new(
            Id: CustomId.New(req.Id),
            DesignerId: User.GetAccountId()
        );
        var order = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        var response = order.ToResponse();
        await SendOkAsync(response).ConfigureAwait(false);
    }
}
