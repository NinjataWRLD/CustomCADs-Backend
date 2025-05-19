namespace CustomCADs.Delivery.Domain.Shipments;

public static class ShipmentValidations
{
	public static Shipment ValidateCountry(this Shipment shipment)
	{
		string property = "Country";
		string country = shipment.Address.Country;

		if (string.IsNullOrEmpty(country))
		{
			throw CustomValidationException<Shipment>.NotNull(property);
		}

		return shipment;
	}

	public static Shipment ValidateCity(this Shipment shipment)
	{
		string property = "City";
		string city = shipment.Address.City;

		if (string.IsNullOrEmpty(city))
		{
			throw CustomValidationException<Shipment>.NotNull(property);
		}

		return shipment;
	}
}
