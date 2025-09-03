namespace CustomCADs.Delivery.Domain.Shipments;

public static class Validations
{
	public static Shipment ValidateCountry(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.Country,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateCity(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.City,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateStreet(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.Street,
				predicate: string.IsNullOrWhiteSpace
			);
}
