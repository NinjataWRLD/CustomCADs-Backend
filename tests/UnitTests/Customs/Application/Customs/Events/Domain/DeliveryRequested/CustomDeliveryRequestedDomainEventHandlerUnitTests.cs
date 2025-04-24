using CustomCADs.Customs.Application.Customs.Events.Domain;
using CustomCADs.Customs.Domain.Customs.Events;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Events.Domain.DeliveryRequested;

using static CustomsData;

public class CustomDeliveryRequestedDomainEventHandlerUnitTests : CustomsBaseUnitTests
{
    private readonly CustomDeliveryRequestedDomainEventHandler handler;
    private readonly Mock<ICustomReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string ShipmentService = "shipment-service";
    private const double Weight = 5.2;
    private const int Count = 3;
    private static readonly CustomId id = ValidId1;
    private static readonly ShipmentId shipmentId = ValidShipmentId2;
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private static readonly ContactDto contact = new("0123456789", null);
    private readonly Custom custom = CreateCustomWithId(id, forDelivery: true);

    public CustomDeliveryRequestedDomainEventHandlerUnitTests()
    {
        handler = new(reads.Object, sender.Object);

        custom.Accept(ValidDesignerId1);
        custom.Begin();
        custom.Finish(ValidCadId1, ValidPrice1);
        custom.Complete(ValidCustomizationId1);

        reads.Setup(x => x.SingleByIdAsync(ValidId1, false, ct))
            .ReturnsAsync(custom);

        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetUsernameByIdQuery>(x => x.Id == custom.BuyerId),
            ct
        )).ReturnsAsync("NinjataBG");

        sender.Setup(x => x.SendCommandAsync(
            It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
            ct
        )).ReturnsAsync(shipmentId);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CustomDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );

        // Act
        await handler.Handle(de);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CustomDeliveryRequestedDomainEvent de = new(
            Id: id,
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
        ), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.Is<CreateShipmentCommand>(x => x.BuyerId == custom.BuyerId),
            ct
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        CustomDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );

        // Act
        await handler.Handle(de);

        // Assert
        Assert.Equal(shipmentId, custom.CompletedCustom?.ShipmentId);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCustomNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId1, false, ct))
            .ReturnsAsync(null as Custom);

        CustomDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(async () =>
        {
            // Act
            await handler.Handle(de);
        });
    }
}
