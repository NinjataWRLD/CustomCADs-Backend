using CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Edit.Data;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Edit;

using static OngoingOrdersData;

public class EditOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    
    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly AccountId buyerId = AccountId.New();
    private readonly OngoingOrder order = CreateOrderWithId(buyerId: buyerId);

    public EditOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);
    }

    [Theory]
    [ClassData(typeof(EditOngoingOrderValidData))]
    public async Task Handle_ShouldQueryDatabase(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );
        EditOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(EditOngoingOrderValidData))]
    public async Task Handle_ShouldPersistToDatabase(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );
        EditOngoingOrderHandler handler = new(reads.Object, uow.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }
}
