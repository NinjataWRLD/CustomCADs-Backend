using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Item.Add;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Add.Data;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Item.Add;

public class AddActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private ActiveCart cart = CreateCart();

    public AddActiveCartItemHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductExistsByIdQuery>(), ct))
            .ReturnsAsync(true);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldQueryDatabase(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, true, ct));
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldPersistToDatabase(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct));
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldSendRequests(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetProductExistsByIdQuery>(x => x.Id == productId),
        ct), Times.Once);
        if (forDelivery)
        {
            sender.Verify(x => x.SendQueryAsync(
                It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == customizationId),
            ct), Times.Once);
        }
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldPopulateProperly(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        cart = CreateCart(buyerId);
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(cart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        var item = cart.Items.First();
        Assert.Multiple(
            () => Assert.Equal(customizationId, item.CustomizationId),
            () => Assert.Equal(forDelivery, item.ForDelivery),
            () => Assert.Equal(productId, item.ProductId)
        );
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldThrowException_WhenProductNotFound(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetProductExistsByIdQuery>(x => x.Id == productId), ct)
        ).ReturnsAsync(false);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldThrowException_WhenCartNotFound(AccountId buyerId, CustomizationId? customizationId, bool forDelivery, ProductId productId)
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, true, ct))
            .ReturnsAsync(null as ActiveCart);

        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            CustomizationId: customizationId,
            ForDelivery: forDelivery,
            ProductId: productId
        );
        AddActiveCartItemHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCart>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
