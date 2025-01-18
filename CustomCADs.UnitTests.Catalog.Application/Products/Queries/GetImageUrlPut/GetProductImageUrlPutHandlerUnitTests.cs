﻿using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlPut;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlPut;

using static ProductsData;

public class GetProductImageUrlPutHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();
    private const string url = "presigned-url";
    private const string contentType = "content-type";
    private const string fileName = "file-name";

    public GetProductImageUrlPutHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetImagePresignedUrlPutByIdQuery>(), ct))
            .ReturnsAsync(url);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId, 
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );
        GetProductImagePresignedUrlPutHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );
        GetProductImagePresignedUrlPutHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetImagePresignedUrlPutByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );
        GetProductImagePresignedUrlPutHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(url, result.PresignedUrl);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidDesignerId
        );
        GetProductImagePresignedUrlPutHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(null as Product);

        GetProductImagePresignedUrlPutQuery query = new(
            Id: ValidId,
            ContentType: contentType,
            FileName: fileName,
            CreatorId: ValidCreatorId
        );
        GetProductImagePresignedUrlPutHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
