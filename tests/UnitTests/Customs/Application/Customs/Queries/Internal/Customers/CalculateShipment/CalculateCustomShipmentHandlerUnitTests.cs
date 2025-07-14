using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

using static CustomsData;

public class CalculateCustomShipmentHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CalculateCustomShipmentHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");

	public CalculateCustomShipmentHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateCustom(forDelivery: true));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		)).ReturnsAsync(0);

		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<CalculateShipmentQuery>(),
			ct
		)).ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CalculateCustomShipmentQuery query = new(ValidId, 0, address, ValidCustomizationId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CalculateCustomShipmentQuery query = new(ValidId, 0, address, ValidCustomizationId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCustomizationWeightByIdQuery>(x => x.Id == ValidCustomizationId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<CalculateShipmentQuery>(x => x.Address == address),
			ct
		), Times.Once());
	}
}
