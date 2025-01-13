﻿using CustomCADs.Orders.Application.OngoingOrders.Commands.Create;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Create.Data;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Create;

using static OngoingOrdersData;

public class CreateOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IWrites<OngoingOrder>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    [Theory]
    [ClassData(typeof(CreateOngoingOrderValidData))]
    public async Task Handle_ShouldPersistToDatabase(string name, string description, bool delivery, AccountId buyerId)
    {
        // Arrange
        CreateOngoingOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            BuyerId: buyerId
        );
        CreateOngoingOrderHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<OngoingOrder>(x =>
            x.Name == name &&
            x.Description == description &&
            x.Delivery == delivery &&
            x.BuyerId == buyerId
        ), ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
