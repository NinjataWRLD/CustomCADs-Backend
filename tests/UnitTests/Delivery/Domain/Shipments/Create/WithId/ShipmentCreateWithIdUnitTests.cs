using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId;

using static ShipmentsData;

public class ShipmentCreateWithIdUnitTests : ShipmentsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		CreateShipmentWithId();
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		Shipment shipment = CreateShipmentWithId();

		Assert.Multiple(
			() => Assert.Equal(new(ValidCountry, ValidCity, ValidStreet), shipment.Address),
			() => Assert.Equal(ValidReferenceId, shipment.ReferenceId),
			() => Assert.Equal(ValidBuyerId, shipment.BuyerId),
			() => Assert.True(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1))
		);
	}

	[Fact]
	public void CreateWithId_ShouldThrowException_WhenCountryIsInvalid()
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipmentWithId(ValidId, InvalidCountry, ValidCity, ValidStreet, ValidReferenceId, ValidBuyerId)
		);
	}

	[Fact]
	public void CreateWithId_ShouldThrowException_WhenCityIsInvalid()
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipmentWithId(ValidId, ValidCountry, InvalidCity, ValidStreet, ValidReferenceId, ValidBuyerId)
		);
	}

	[Fact]
	public void CreateWithId_ShouldThrowException_WhenStreetIsInvalid()
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipmentWithId(ValidId, ValidCountry, ValidCity, InvalidStreet, ValidReferenceId, ValidBuyerId)
		);
	}
}
