using CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Waybill;

using static Constants.Roles;

public class GetShipmentWaybillEndpoint(IRequestSender sender)
    : Endpoint<GetShipmentWaybillRequest>
{
    public override void Configure()
    {
        Get("shipments/{shipmentId}/waybill");
        Roles(Designer);
        Description(d => d
            .WithTags("06. Shipments")
            .WithSummary("03. Get Shipment waybill")
            .WithDescription("Download this Shipment's waybill")
        );
    }

    public override async Task HandleAsync(GetShipmentWaybillRequest req, CancellationToken ct)
    {
        GetShipmentWaybillQuery query = new(
            ShipmentId: req.ShipmentId,
            DesignerId: User.GetAccountId()
        );
        byte[] bytes = await sender.SendQueryAsync(query, ct);
        await SendBytesAsync(bytes, "waybill.pdf", "application/pdf");
    }
}
