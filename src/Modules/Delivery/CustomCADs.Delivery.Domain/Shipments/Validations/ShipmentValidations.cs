using CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;

namespace CustomCADs.Delivery.Domain.Shipments.Validations;

public static class ShipmentValidations
{
    public static Shipment ValidateCountry(this Shipment shipment)
    {
        string property = "Country";
        string country = shipment.Address.Country;

        if (string.IsNullOrEmpty(country))
        {
            throw ShipmentValidationException.NotNull(property);
        }

        return shipment;
    }

    public static Shipment ValidateCity(this Shipment shipment)
    {
        string property = "City";
        string city = shipment.Address.City;

        if (string.IsNullOrEmpty(city))
        {
            throw ShipmentValidationException.NotNull(property);
        }

        return shipment;
    }
}
