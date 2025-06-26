using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

using Data;
using static ActiveCartsData;

public class CalculateActiveCartShipmentValidatorUnitTests : ActiveCartsBaseUnitTests
{
	private readonly CalculateActiveCartShipmentValidator validator = new();

	[Theory]
	[ClassData(typeof(CalculateActiveCartShipmentValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(AddressDto address)
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(
			BuyerId: ValidBuyerId,
			Address: address
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(CalculateActiveCartShipmentInvalidCountryData))]
	[ClassData(typeof(CalculateActiveCartShipmentInvalidCityData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(AddressDto address)
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(
			BuyerId: ValidBuyerId,
			Address: address
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(CalculateActiveCartShipmentInvalidCountryData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(AddressDto address)
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(
			BuyerId: ValidBuyerId,
			Address: address
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.Country);
	}

	[Theory]
	[ClassData(typeof(CalculateActiveCartShipmentInvalidCityData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(AddressDto address)
	{
		// Arrange
		CalculateActiveCartShipmentQuery query = new(
			BuyerId: ValidBuyerId,
			Address: address
		);

		// Act
		var result = await validator.TestValidateAsync(query, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.City);
	}
}
