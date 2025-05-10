using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Add;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Add;

using Data;
using static ActiveCartsData;

public class AddActiveCartItemHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly AddActiveCartItemHandler handler;
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<ActiveCartItem>> writes = new();
    private readonly Mock<IRequestSender> sender = new();

    public AddActiveCartItemHandlerUnitTests()
    {
        handler = new(writes.Object, uow.Object, sender.Object);

        sender.Setup(x => x.SendQueryAsync(
            It.IsAny<GetProductExistsByIdQuery>(),
            ct
        )).ReturnsAsync(true);

        sender.Setup(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationExistsByIdQuery>(),
            ct
        )).ReturnsAsync(true);
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldPersistToDatabase(CustomizationId? customizationId)
    {
        // Arrange
        AddActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            CustomizationId: customizationId,
            ForDelivery: customizationId is not null,
            ProductId: ValidProductId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct));
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldSendRequests(CustomizationId? customizationId)
    {
        // Arrange
        AddActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            CustomizationId: customizationId,
            ForDelivery: customizationId is not null,
            ProductId: ValidProductId
        );

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetProductExistsByIdQuery>(x => x.Id == ValidProductId),
            ct
        ), Times.Once);

        if (customizationId is not null)
        {
            sender.Verify(x => x.SendQueryAsync(
                It.Is<GetCustomizationExistsByIdQuery>(x => x.Id == customizationId),
                ct
            ), Times.Once);
        }
    }

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Handle_ShouldThrowException_WhenProductNotFound(CustomizationId? customizationId)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(
            It.Is<GetProductExistsByIdQuery>(x => x.Id == ValidProductId),
            ct
        )).ReturnsAsync(false);

        AddActiveCartItemCommand command = new(
            BuyerId: ValidBuyerId,
            CustomizationId: customizationId,
            ForDelivery: customizationId is not null,
            ProductId: ValidProductId
        );

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<ActiveCartItem>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
