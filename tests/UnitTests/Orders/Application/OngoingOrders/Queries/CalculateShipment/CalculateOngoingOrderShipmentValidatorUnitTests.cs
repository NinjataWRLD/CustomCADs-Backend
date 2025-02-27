using CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.CalculateShipment.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.CalculateShipment;

using static OngoingOrdersData;

public class CalculateOngoingOrderShipmentValidatorUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly CalculateOngoingOrderShipmentValidator validator = new();

    private const int Count = 4;
    private const double Weight = 2.7;
    private static readonly OngoingOrderId id = ValidId1;

    [Theory]
    [ClassData(typeof(CalculateOngoingOrderShipmentValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string country, string city)
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            TotalCount: Count,
            TotalWeight: Weight,
            Address: new(country, city)
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CalculateOngoingOrderShipmentInvalidCountryData))]
    [ClassData(typeof(CalculateOngoingOrderShipmentInvalidCityData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string country, string city)
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            TotalCount: Count,
            TotalWeight: Weight,
            Address: new(country, city)
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CalculateOngoingOrderShipmentInvalidCountryData))]
    public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string country, string city)
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            TotalCount: Count,
            TotalWeight: Weight,
            Address: new(country, city)
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.Country);
    }

    [Theory]
    [ClassData(typeof(CalculateOngoingOrderShipmentInvalidCityData))]
    public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string country, string city)
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            TotalCount: Count,
            TotalWeight: Weight,
            Address: new(country, city)
        );

        // Act
        var result = await validator.TestValidateAsync(query, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.City);
    }
}
