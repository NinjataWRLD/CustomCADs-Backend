using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create;

using static CompletedOrdersData;

public class CreateCompletedOrderHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<IWrites<CompletedOrder>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldPersistToDatabase(string name, string description, decimal price, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderDate: orderDate,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<CompletedOrder>(x => 
            x.Name == name &&
            x.Description == description &&
            x.Delivery == delivery &&
            x.OrderDate == orderDate &&
            x.BuyerId == buyerId &&
            x.DesignerId == designerId &&
            x.CadId == cadId
        ) , ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
