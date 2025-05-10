using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;

using Data;
using static ShipmentsData;

public class ShipmentCreateUnitTests : ShipmentsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ShipmentCreateValidData))]
    public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid(string country, string city, string referenceId)
    {
        Shipment.Create(new(country, city), referenceId, ValidBuyerId);
    }

    [Theory]
    [ClassData(typeof(ShipmentCreateValidData))]
    public void Create_ShouldPopulatePropertiesProperly_WhenShipmentIsValid(string country, string city, string referenceId)
    {
        Address address = new(country, city);
        var shipment = Shipment.Create(address, referenceId, ValidBuyerId);

        Assert.Multiple(
            () => Assert.Equal(address, shipment.Address),
            () => Assert.Equal(referenceId, shipment.ReferenceId),
            () => Assert.Equal(ValidBuyerId, shipment.BuyerId),
            () => Assert.True(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1))
        );
    }

    [Theory]
    [ClassData(typeof(ShipmentCreateInvalidCountryData))]
    public void Create_ShouldThrowException_WhenCountryIsInvalid(string country, string city, string referenceId)
    {
        Assert.Throws<CustomValidationException<Shipment>>(() =>
        {
            Shipment.Create(new(country, city), referenceId, ValidBuyerId);
        });
    }

    [Theory]
    [ClassData(typeof(ShipmentCreateInvalidCityData))]
    public void Create_ShouldThrowException_WhenCityIsInvalid(string country, string city, string referenceId)
    {
        Assert.Throws<CustomValidationException<Shipment>>(() =>
        {
            Shipment.Create(new(country, city), referenceId, ValidBuyerId);
        });
    }
}
