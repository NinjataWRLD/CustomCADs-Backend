
namespace CustomCADs.Speedy.Http.Dtos.ShipmentParcels;

/// <param name="SeqNo">
///     Parcel sequence number in the shipment.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="Id">
///     Parcel identifier.
///     <br />
///     Required: Yes.
/// </param>
/// <param name="ExternalCarrierId">
///     External carrier id, if applicable.
///     <br />
///     Required: No.
/// </param>
/// <param name="ExternalCarrierParcelNumber">
///     External carrier parcel number, if applicable.
///     <br />
///     Required: No.
/// </param>
internal record CreatedShipmentParcelDto(
	int SeqNo,
	string Id,
	int? ExternalCarrierId,
	string? ExternalCarrierParcelNumber
);
