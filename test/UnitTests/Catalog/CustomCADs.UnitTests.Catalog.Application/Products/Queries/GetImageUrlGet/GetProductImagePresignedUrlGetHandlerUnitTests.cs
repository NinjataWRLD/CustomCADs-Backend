using CustomCADs.Catalog.Application.Products.Queries.GetImageUrlGet;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetImageUrlGet;

using static ProductsData;

public class GetProductImagePresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();
    private const string url = "presigned-url";
    private const string contentType = "application/xml";

    public GetProductImagePresignedUrlGetHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetImagePresignedUrlGetByIdQuery>(), ct))
            .ReturnsAsync((url, contentType));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetProductImagePresignedUrlGetQuery query = new(ValidId);
        GetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetProductImagePresignedUrlGetQuery query = new(ValidId);
        GetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetImagePresignedUrlGetByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetProductImagePresignedUrlGetQuery query = new(ValidId);
        GetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(url, result.PresignedUrl),
            () => Assert.Equal(contentType, result.ContentType)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenProductNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(null as Product);

        GetProductImagePresignedUrlGetQuery query = new(ValidId);
        GetProductImagePresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
