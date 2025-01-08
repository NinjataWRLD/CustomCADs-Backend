﻿using CustomCADs.Catalog.Application.Products.Commands.Create;
using CustomCADs.Catalog.Domain.Common;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Shared.Application.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Categories;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Images.Commands;
using CustomCADs.UnitTests.Catalog.Application.Products.Commands.Create.Data;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Create;

using static Constants.Roles;
using static ProductsData;

public class CreateProductHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IWrites<Product>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly CategoryId categoryId = ValidCategoryId;
    private readonly AccountId creatorId = ValidCreatorId;
    private readonly ImageId imageId = ValidImageId;
    private readonly CadId cadId = ValidCadId;

    public CreateProductHandlerUnitTests()
    {
        sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateCadCommand>(), ct))
            .ReturnsAsync(cadId);

        sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateImageCommand>(), ct))
            .ReturnsAsync(imageId);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct))
            .ReturnsAsync(Contributor);
    }

    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public async Task Handler_ShouldPersistToDatabase(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CategoryId: categoryId,
            CreatorId: creatorId
        );
        CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Product>(x =>
                x.Name == name &&
                x.Description == description &&
                x.Price == price &&
                x.Status == ProductStatus.Unchecked &&
                x.CreatorId == creatorId &&
                x.CategoryId == categoryId &&
                x.ImageId == imageId &&
                x.CadId == cadId
            )
        , ct), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public async Task Handler_ShouldSendRequests(string name, string description, decimal price)
    {
        // Arrange
        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CategoryId: categoryId,
            CreatorId: creatorId
        );
        CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendCommandAsync(It.IsAny<CreateCadCommand>(), ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(It.IsAny<CreateImageCommand>(), ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateProductValidData))]
    public async Task Handler_ShouldSetStatusProperlty(string name, string description, decimal price)
    {
        // Arrange
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUserRoleByIdQuery>(), ct))
            .ReturnsAsync(Designer);

        CreateProductCommand command = new(
            Name: name,
            Description: description,
            Price: price,
            ImageKey: string.Empty,
            ImageContentType: string.Empty,
            CadKey: string.Empty,
            CadContentType: string.Empty,
            CategoryId: categoryId,
            CreatorId: creatorId
        );
        CreateProductHandler handler = new(writes.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Product>(x => x.Status == ProductStatus.Validated)
        , ct), Times.Once);
    }
}
