using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal.Data;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId;

public class ShipmentCreateWithIdUnitTests : ShipmentsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ShipmentCreateWithIdValidData))]
    public void CreateWithId_ShouldNotThrowExcepion_WhenShipmentIsValid(string country, string city, string referenceId, AccountId buyerId)
    {
        Shipment.Create(new(country, city), referenceId, buyerId);
    }

    [Theory]
    [ClassData(typeof(ShipmentCreateWithIdValidData))]
    public void CreateWithId_ShouldPopulatePropertiesProperly_WhenShipmentIsValid(string country, string city, string referenceId, AccountId buyerId)
    {
        Address address = new(country, city);
        var shipment = Shipment.Create(address, referenceId, buyerId);

        Assert.Multiple(
            () => Assert.Equal(address, shipment.Address),
            () => Assert.Equal(referenceId, shipment.ReferenceId),
            () => Assert.Equal(buyerId, shipment.BuyerId),
            () => Assert.True(DateTime.UtcNow - shipment.RequestDate < TimeSpan.FromSeconds(1))
        );
    }

    [Theory]
    [ClassData(typeof(ShipmentCreateWithIdInvalidCountryData))]
    [ClassData(typeof(ShipmentCreateWithIdInvalidCityData))]
    public void CreateWithId_ShouldThrowException_WhenShipmentIsInvalid(string country, string city, string referenceId, AccountId buyerId)
    {
        Assert.Throws<ShipmentValidationException>(() =>
        {
            Shipment.Create(new(country, city), referenceId, buyerId);
        });
    }
}
