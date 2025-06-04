using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId;

using Data;
using static ShipmentsData;

public class ShipmentCreateWithIdUnitTests : ShipmentsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdValidData))]
	public void CreateWithId_ShouldNotThrowExcepion_WhenShipmentIsValid(string country, string city, string referenceId)
	{
		Shipment.Create(new(country, city), referenceId, ValidBuyerId);
	}

	[Theory]
	[ClassData(typeof(ShipmentCreateWithIdValidData))]
	public void CreateWithId_ShouldPopulatePropertiesProperly_WhenShipmentIsValid(string country, string city, string referenceId)
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
	[ClassData(typeof(ShipmentCreateWithIdInvalidCountryData))]
	[ClassData(typeof(ShipmentCreateWithIdInvalidCityData))]
	public void CreateWithId_ShouldThrowException_WhenShipmentIsInvalid(string country, string city, string referenceId)
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => Shipment.Create(new(country, city), referenceId, ValidBuyerId)
		);
	}
}
