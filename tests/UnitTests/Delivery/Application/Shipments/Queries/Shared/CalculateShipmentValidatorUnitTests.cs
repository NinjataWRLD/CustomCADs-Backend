using CustomCADs.Delivery.Application.Shipments.Queries.Shared;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Queries;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared;

public class CalculateShipmentValidatorUnitTests : ShipmentsBaseUnitTests
{
	private readonly CalculateShipmentValidator validator = new();

	private static readonly double[] weights = [0, 1, 2, 3, 4, 5, 6];
	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");

	[Fact]
	public async Task Validate_ShouldBeValid_WhenAddressIsValid()
	{
		// Arrange
		CalculateShipmentQuery query = new(weights, address);

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CalculateShipmentInvalidData))]
	public async Task Validate_ShouldBeInvalid_WhenAddressIsInvalid(string country, string city, string street)
	{
		// Arrange
		CalculateShipmentQuery query = new(weights, Address: new(country, city, street));

		// Act
		var result = await validator.TestValidateAsync(query);

		// Assert
		Assert.False(result.IsValid);
	}
}
