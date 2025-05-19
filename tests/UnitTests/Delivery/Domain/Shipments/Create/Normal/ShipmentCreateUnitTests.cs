using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal.Data;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;

public class ShipmentCreateUnitTests : ShipmentsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdValidData))]
	public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid(string country, string city, string referenceId, AccountId buyerId)
	{
		Shipment.Create(new(country, city), referenceId, buyerId);
	}

	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdValidData))]
	public void Create_ShouldPopulatePropertiesProperly_WhenShipmentIsValid(string country, string city, string referenceId, AccountId buyerId)
	{
		Address address = new(country, city);
		var shipment = Shipment.Create(address, referenceId, buyerId);

		Assert.Multiple(
			() => Assert.Equal(address, shipment.Address),
			() => Assert.Equal(referenceId, shipment.ReferenceId),
			() => Assert.Equal(buyerId, shipment.BuyerId),
			() => Assert.True(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1))
		);
	}

	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdInvalidCountryData))]
	public void Create_ShouldThrowException_WhenCountryIsInvalid(string country, string city, string referenceId, AccountId buyerId)
	{
		Assert.Throws<CustomValidationException<Shipment>>(() =>
		{
			Shipment.Create(new(country, city), referenceId, buyerId);
		});
	}

	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdInvalidCityData))]
	public void Create_ShouldThrowException_WhenCityIsInvalid(string country, string city, string referenceId, AccountId buyerId)
	{
		Assert.Throws<CustomValidationException<Shipment>>(() =>
		{
			Shipment.Create(new(country, city), referenceId, buyerId);
		});
	}
}
