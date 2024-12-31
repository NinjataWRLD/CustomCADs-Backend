using CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Waybill;

using static Constants.Roles;

public class GetShipmentWaybillEndpoint(IRequestSender sender)
    : Endpoint<GetShipmentWaybillRequest>
{
    public override void Configure()
    {
        Get("{id}/waybill");
        Group<ShipmentGroup>();
        Roles(Designer);
        Description(d => d
            .WithSummary("04. Get Shipment waybill")
            .WithDescription("Download this Shipment's waybill")
        );
    }

    public override async Task HandleAsync(GetShipmentWaybillRequest req, CancellationToken ct)
    {
        GetShipmentWaybillQuery query = new(
            Id: new ShipmentId(req.Id),
            DesignerId: User.GetAccountId()
        );
        byte[] bytes = await sender.SendQueryAsync(query, ct).ConfigureAwait(false);

        await SendBytesAsync(bytes, "waybill.pdf", "application/pdf").ConfigureAwait(false);
    }
}
