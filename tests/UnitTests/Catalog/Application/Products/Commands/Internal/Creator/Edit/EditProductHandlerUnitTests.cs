﻿using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Edit;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Edit;

using static ProductsData;

public class EditProductHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();

    public EditProductHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCategoryExistsByIdQuery>(), ct))
            .ReturnsAsync(true);
    }

    [Fact]
    public async Task Handler_ShouldQueryDatabase()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldPersistToDatabase()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldSendRequests()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId)
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidDesignerId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenCategoryNotFound()
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCategoryExistsByIdQuery>(), ct))
            .ReturnsAsync(false);

        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidCreatorId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
            .ReturnsAsync(null as Product);

        EditProductCommand command = new(
            Id: ValidId,
            Name: ValidName1,
            Description: ValidDescription1,
            Price: ValidPrice1,
            CategoryId: ValidCategoryId,
            CreatorId: ValidDesignerId
        );
        EditProductHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
