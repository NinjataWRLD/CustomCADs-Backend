using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Designer.SetStatus;

using static ProductsData;

public class SetProductStatusHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly SetProductStatusHandler handler;
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    private readonly Product product = CreateProduct();
    private const ProductStatus status = ProductStatus.Validated;

    public SetProductStatusHandlerUnitTests()
    {
        handler = new(reads.Object, uow.Object, sender.Object);

        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersisttoDatabase()
    {
        // Arrange
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidDesignerId)
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        product.SetDesignerId(ValidDesignerId);
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenDesignerNotFound()
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidDesignerId), ct))
            .ReturnsAsync(false);
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(null as Product);
        SetProductStatusCommand command = new(ValidId, status, ValidDesignerId);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
