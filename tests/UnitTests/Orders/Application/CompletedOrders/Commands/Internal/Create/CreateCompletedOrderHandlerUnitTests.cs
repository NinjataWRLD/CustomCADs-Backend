using CustomCADs.Orders.Application.CompletedOrders.Commands.Internal.Create;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Internal.Create.Data;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Internal.Create;

using static CompletedOrdersData;

public class CreateCompletedOrderHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<IWrites<CompletedOrder>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    public CreateCompletedOrderHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
            .ReturnsAsync(true);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationExistsByIdQuery>(), ct))
            .ReturnsAsync(true);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldPersistToDatabase(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId, CustomizationId? customizationId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderedAt: orderedAt,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId,
            CustomizationId: customizationId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<CompletedOrder>(x =>
            x.Name == name &&
            x.Description == description &&
            x.Delivery == delivery &&
            x.OrderedAt == orderedAt &&
            x.BuyerId == buyerId &&
            x.DesignerId == designerId &&
            x.CadId == cadId
        ), ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldSentRequests(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId, CustomizationId? customizationId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderedAt: orderedAt,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId,
            CustomizationId: customizationId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId)
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId)
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetCadExistsByIdQuery>(x => x.Id == cadId)
        , ct), Times.Once);
        if (delivery)
        {
            sender.Verify(x => x.SendQueryAsync(
                It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == customizationId)
            , ct), Times.Once);
        }
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldThrowException_WhenBuyerNotFound(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId, CustomizationId? customizationId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId), ct))
            .ReturnsAsync(false);

        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderedAt: orderedAt,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId,
            CustomizationId: customizationId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<CompletedOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldThrowException_WhenDesignerNotFound(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId, CustomizationId? customizationId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == designerId), ct))
            .ReturnsAsync(false);

        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderedAt: orderedAt,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId,
            CustomizationId: customizationId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<CompletedOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Handle_ShouldThrowException_WhenCadNotFound(string name, string description, decimal price, bool delivery, DateTimeOffset orderedAt, AccountId buyerId, AccountId designerId, CadId cadId, CustomizationId? customizationId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetCadExistsByIdQuery>(x => x.Id == cadId), ct))
            .ReturnsAsync(false);

        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            Delivery: delivery,
            OrderedAt: orderedAt,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId,
            CustomizationId: customizationId
        );
        CreateCompletedOrderHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<CompletedOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
