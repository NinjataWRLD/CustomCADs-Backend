using CustomCADs.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;

using static ShipmentsData;

public class ShipmentCreateUnitTests : ShipmentsBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		Shipment.Create(new(ValidCountry, ValidCity), ValidReferenceId, ValidBuyerId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		Address address = new(ValidCountry, ValidCity);
		var shipment = Shipment.Create(address, ValidReferenceId, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(address, shipment.Address),
			() => Assert.Equal(ValidReferenceId, shipment.ReferenceId),
			() => Assert.Equal(ValidBuyerId, shipment.BuyerId),
			() => Assert.True(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1))
		);
	}

	[Fact]
	public void Create_ShouldThrowException_WhenCountryIsInvalid()
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipment(InvalidCountry, ValidCity, ValidReferenceId, ValidBuyerId)
		);
	}

	[Fact]
	public void Create_ShouldThrowException_WhenCityIsInvalid()
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipment(ValidCountry, InvalidCity, ValidReferenceId, ValidBuyerId)
		);
	}
}
