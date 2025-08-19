namespace CustomCADs.Speedy.Http.Dtos.CalculationContent;

using ShipmentContent.ShipmentParcel;

/// <remarks>
/// 	Calculation content is used to describe what is to be delivered with the shipment for calculation purposes.
/// </remarks>
/// <param name="ParcelsCount">
/// 	Total shipment parcels count. Ignored, if parcels list is not empty.
/// 	<br />
/// 	Required: when parcels list is empty
/// </param>
/// <param name="TotalWeight">
/// 	Summary: Total weight declared for the shipment. Ignored, if parcels list is not empty. The total weight is the sum of all parcels declaredWeight in that case.
/// 	<br />
/// 	Required: when parcels list is empty. Validated against the minimum and maximum allowed for the service.
/// </param>
/// <param name="Documents">
/// 	Summary: Documents flag of the shipment
/// 	<br />
/// 	Required: No
/// </param>
/// <param name="Palletized">
/// 	Summary: Palletized flag of the shipment.
/// 	<br />
/// 	Required: No. Validated against the minimum and maximum allowed for the service.
/// </param>
/// <param name="Parcels">
/// 	Summary: List of parcels
/// 	<br />
/// 	Required: for pallet and postal services. Validated against the service configuration and pickup and delivery capacity of the depots and couriers.
/// </param>
internal record CalculationContentDto(
	int? ParcelsCount,
	double? TotalWeight,
	bool? Documents,
	bool? Palletized,
	ShipmentParcelDto[]? Parcels
);
