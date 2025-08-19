namespace CustomCADs.Speedy.Http.Dtos.ShipmentService.ShipmentAdditionalServices;

using ShipmentCodAdditionalService;
using ShipmentDeclaredValueAdditionalService;
using ShipmentObpd;
using ShipmentReturnAdditionalServices;

/// <param name="Cod">
///     COD sub-service. Defines shipment COD base amount, currency, payout options, payment type, and fiscal receipt items.
///     <br />
///     Required: No. Seller may need valid contract and annex for COD for the destination.
///     See ShipmentCodAdditionalServiceDto for details.
/// </param>
/// <param name="Obdp">
///     Options before payment details. Defines recipient options on delivery before COD payment.
///     <br />
///     Required: No. See ShipmentObpdDto for details.
/// </param>
/// <param name="DeclaredValue">
///     Declared value (extended liability) sub-service. Defines base amount, fragile flag, and ignore flag.
///     <br />
///     Required: No. See ShipmentDeclaredValueAdditionalServiceDto for details.
/// </param>
/// <param name="FixedTimeDelivery">
///     Fixed time delivery option. Fixes the time of delivery on the delivery date (e.g., 1130 for 11:30).
///     <br />
///     Required: No. Checked against allowed time frame for the service.
/// </param>
/// <param name="Returns">
///     Return sub-services. Defines series of return sub-services (ROD, Return Receipt, SWAP, ROP, Return Voucher, etc.).
///     <br />
///     Required: No. See ShipmentReturnAdditionalServicesDto for details.
/// </param>
/// <param name="SpecialDeliveryId">
///     Special delivery identifier. Used for shipments with special delivery annex.
///     <br />
///     Required: No. Requires annex for special delivery.
/// </param>
/// <param name="DeliveryToFloor">
///     Delivery to floor number in the building.
///     <br />
///     Required: No. Requires annex.
/// </param>
internal record ShipmentAdditionalServicesDto(
	ShipmentCodAdditionalServiceDto? Cod,
	ShipmentObpdDto? Obdp,
	ShipmentDeclaredValueAdditionalServiceDto? DeclaredValue,
	int? FixedTimeDelivery,
	ShipmentReturnAdditionalServicesDto? Returns,
	int? SpecialDeliveryId,
	int? DeliveryToFloor
);
