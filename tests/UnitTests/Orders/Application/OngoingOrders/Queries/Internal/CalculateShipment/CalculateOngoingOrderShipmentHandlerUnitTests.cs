﻿using CustomCADs.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Queries.Internal.CalculateShipment;

using static OngoingOrdersData;

public class CalculateOngoingOrderShipmentHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string TimeZone = "Europe/Sofia";
    private static readonly CustomizationId CustomizationId = CustomizationId.New();
    private const int Count = 3;
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private readonly OngoingOrder order = CreateOrderWithId(id, delivery: true);
    private readonly CalculateShipmentDto calculation = new(
        Total: 0,
        Currency: string.Empty,
        Service: string.Empty,
        PickupDate: default,
        DeliveryDeadline: default
    );

    public CalculateOngoingOrderShipmentHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<CalculateShipmentQuery>(), ct))
            .ReturnsAsync([calculation]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetTimeZoneByIdQuery>(), ct))
            .ReturnsAsync(TimeZone);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            CustomizationId: CustomizationId,
            Count: Count,
            Address: address
        );
        CalculateOngoingOrderShipmentHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            CustomizationId: CustomizationId,
            Count: Count,
            Address: address
        );
        CalculateOngoingOrderShipmentHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationWeightByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<CalculateShipmentQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetTimeZoneByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            CustomizationId: CustomizationId,
            Count: Count,
            Address: address
        );
        CalculateOngoingOrderShipmentHandler handler = new(reads.Object, sender.Object);

        // Act
        var calculations = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Single(calculations),
            () => Assert.Equal(calculation.Service, calculations.First().Service),
            () => Assert.Equal(calculation.Currency, calculations.First().Currency),
            () => Assert.Equal(calculation.Total, calculations.First().Total),
            () => Assert.Equal(calculation.PickupDate, calculations.First().PickupDate),
            () => Assert.Equal(calculation.DeliveryDeadline, calculations.First().DeliveryDeadline)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderDoesNotHaveDelivery()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(CreateOrderWithId(id, delivery: false));

        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            CustomizationId: CustomizationId,
            Count: Count,
            Address: address
        );
        CalculateOngoingOrderShipmentHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as OngoingOrder);

        CalculateOngoingOrderShipmentQuery query = new(
            Id: id,
            CustomizationId: CustomizationId,
            Count: Count,
            Address: address
        );
        CalculateOngoingOrderShipmentHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
