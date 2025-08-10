using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Delivery.Application.Shipments.Queries.Shared;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared;

public class CalculateShipmentHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly CalculateShipmentHandler handler;
	private readonly Mock<IDeliveryService> delivery = new();

	private static readonly CalculationDto[] calculations = [
		new(string.Empty, new(default, default, default, string.Empty), default, default)
	];
	private static readonly double[] weights = [0, 1, 2, 3, 4, 5, 6];
	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");

	public CalculateShipmentHandlerUnitTests()
	{
		handler = new(delivery.Object);

		delivery.Setup(x => x.CalculateAsync(
			It.Is<CalculateRequest>(x =>
				x.Country == address.Country
				&& x.City == address.City
				&& x.Street == address.Street
				&& x.Weights.Length == weights.Length
			),
			ct
		)).ReturnsAsync(calculations);
	}

	[Fact]
	public async Task Handle_ShouldCallDelivery()
	{
		// Arrange
		CalculateShipmentQuery query = new(weights, address);

		// Act
		await handler.Handle(query, ct);

		// Assert
		delivery.Verify(x => x.CalculateAsync(
			It.Is<CalculateRequest>(x =>
				x.Country == address.Country
				&& x.City == address.City
				&& x.Street == address.Street
				&& x.Weights.Length == weights.Length
			),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CalculateShipmentQuery query = new(weights, address);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(calculations.Length, result.Length);
	}
}
