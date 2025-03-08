using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.OngoingOrders.Commands.Status.Finish;
using CustomCADs.Orders.Domain.Common;
using CustomCADs.Orders.Domain.OngoingOrders.Enums;
using CustomCADs.Orders.Domain.OngoingOrders.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Status.Finish;

using static OngoingOrdersData;

public class FinishOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string Key = "generated-key";
    private const string ContentType = "model/gltf-binary";
    private const decimal Volume = 15;
    private const decimal Price = ValidPrice1;
    private static readonly OngoingOrderId id = ValidId1;
    private static readonly CadId cadId = ValidCadId1;
    private static readonly AccountId designerId = ValidDesignerId1;
    private static readonly AccountId wrongDesignerId = ValidDesignerId2;
    private readonly (string Key, string ContentType, decimal Volume) cad = (Key, ContentType, Volume);
    private readonly OngoingOrder order = CreateOrder()
        .SetAcceptedStatus().SetDesignerId(designerId).SetBegunStatus();
    private readonly OngoingOrder orderWithWrongDesigner = CreateOrder()
        .SetAcceptedStatus().SetDesignerId(wrongDesignerId).SetBegunStatus();

    public FinishOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendCommandAsync(
            It.IsAny<CreateCadCommand>()
        , ct)).ReturnsAsync(cadId);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        FinishOngoingOrderCommand command = new(
            Id: id,
            Cad: cad,
            Price: Price,
            DesignerId: designerId
        );
        FinishOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        FinishOngoingOrderCommand command = new(
            Id: id,
            Cad: cad,
            Price: Price,
            DesignerId: designerId
        );
        FinishOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        FinishOngoingOrderCommand command = new(
            Id: id,
            Cad: cad,
            Price: Price,
            DesignerId: designerId
        );
        FinishOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(cadId, order.CadId),
            () => Assert.Equal(OngoingOrderStatus.Finished, order.OrderStatus)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(orderWithWrongDesigner);

        FinishOngoingOrderCommand command = new(
            Id: id,
            Cad: cad,
            Price: Price,
            DesignerId: designerId
        );
        FinishOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as OngoingOrder);

        FinishOngoingOrderCommand command = new(
            Id: id,
            Cad: cad,
            Price: Price,
            DesignerId: designerId
        );
        FinishOngoingOrderHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
