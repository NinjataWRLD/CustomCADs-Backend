using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create;

public class ShipmentCreateUnitTests : ShipmentsBaseUnitTests
{
    [Theory]
    [InlineData(ValidCountry1, ValidCity1, ValidReferenceId, ValidBuyerId)]
    [InlineData(ValidCountry2, ValidCity2, ValidReferenceId, ValidBuyerId)]
    public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid(string country, string city, string referenceId, string buyerId)
    {
        Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
    }
    
    [Theory]
    [InlineData(ValidCountry1, ValidCity1, ValidReferenceId, ValidBuyerId)]
    [InlineData(ValidCountry2, ValidCity2, ValidReferenceId, ValidBuyerId)]
    public void Create_ShouldPopulatePropertiesProperly_WhenShipmentIsValid(string country, string city, string referenceId, string buyerId)
    {
        Address address = new(country, city);
        AccountId buyerAccountId = new(Guid.Parse(buyerId));
        var shipment = Shipment.Create(address, referenceId, buyerAccountId);

        Assert.Multiple(
            () => Assert.Equal(address, shipment.Address),
            () => Assert.Equal(referenceId, shipment.ReferenceId),
            () => Assert.Equal(buyerAccountId, shipment.BuyerId),
            () => Assert.True(DateTime.UtcNow - shipment.RequestDate < TimeSpan.FromSeconds(1))
        );
    }
    
    [Theory]
    [InlineData(InvalidCountry, ValidCity1, ValidReferenceId, ValidBuyerId)]
    [InlineData(InvalidCountry, ValidCity2, ValidReferenceId, ValidBuyerId)]
    public void Create_ShouldThrowException_WhenCountryIsInvalid(string country, string city, string referenceId, string buyerId)
    {
        Assert.Throws<ShipmentValidationException>(() =>
        {
            Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
        });
    }
    
    [Theory]
    [InlineData(ValidCountry1, InvalidCity, ValidReferenceId, ValidBuyerId)]
    [InlineData(ValidCountry2, InvalidCity, ValidReferenceId, ValidBuyerId)]
    public void Create_ShouldThrowException_WhenCityIsInvalid(string country, string city, string referenceId, string buyerId)
    {
        Assert.Throws<ShipmentValidationException>(() =>
        {
            Shipment.Create(new(country, city), referenceId, new(Guid.Parse(buyerId)));
        });
    }
}
