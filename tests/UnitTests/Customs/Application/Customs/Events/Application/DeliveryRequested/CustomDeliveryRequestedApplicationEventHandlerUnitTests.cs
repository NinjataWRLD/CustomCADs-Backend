using CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Events.Application.DeliveryRequested;

using static CustomsData;

public class CustomDeliveryRequestedApplicationEventHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CustomDeliveryRequestedApplicationEventHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string ShipmentService = "shipment-service";
	private const double Weight = 5.2;
	private const int Count = 3;
	private static readonly AddressDto address = new("Bulgaria", "Burgas", "Slivnitsa");
	private static readonly ContactDto contact = new("0123456789", null);
	private readonly Custom custom = CreateCustomWithId(ValidId, forDelivery: true);

	public CustomDeliveryRequestedApplicationEventHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(ValidCustomizationId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == custom.BuyerId),
			ct
		)).ReturnsAsync("NinjataBG");

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
			ct
		)).ReturnsAsync(ValidShipmentId);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CustomDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: ShipmentService,
			Weight: Weight,
			Count: Count,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(de);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CustomDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: ShipmentService,
			Weight: Weight,
			Count: Count,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == custom.BuyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		CustomDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: ShipmentService,
			Weight: Weight,
			Count: Count,
			Address: address,
			Contact: contact
		);

		// Act
		await handler.Handle(de);

		// Assert
		Assert.Equal(ValidShipmentId, custom.CompletedCustom?.ShipmentId);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Custom);

		CustomDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: ShipmentService,
			Weight: Weight,
			Count: Count,
			Address: address,
			Contact: contact
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(de)
		);
	}
}
