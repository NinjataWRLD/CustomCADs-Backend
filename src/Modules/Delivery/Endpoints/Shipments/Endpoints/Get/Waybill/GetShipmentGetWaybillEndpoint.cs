namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Waybill;

using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;
using static Constants.Roles;

public class GetShipmentWaybillEndpoint(IRequestSender sender)
	: Endpoint<GetShipmentWaybillRequest>
{
	public override void Configure()
	{
		Get("{id}/waybill");
		Group<ShipmentsGroup>();
		Roles(Designer);
		Description(d => d
			.WithSummary("Waybill")
			.WithDescription("Download this Shipment's waybill")
		);
	}

	public override async Task HandleAsync(GetShipmentWaybillRequest req, CancellationToken ct)
	{
		byte[] bytes = await sender.SendQueryAsync(
			new GetShipmentWaybillQuery(
				Id: ShipmentId.New(req.Id),
				DesignerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);

		await SendBytesAsync(bytes, "waybill.pdf", "application/pdf").ConfigureAwait(false);
	}
}
