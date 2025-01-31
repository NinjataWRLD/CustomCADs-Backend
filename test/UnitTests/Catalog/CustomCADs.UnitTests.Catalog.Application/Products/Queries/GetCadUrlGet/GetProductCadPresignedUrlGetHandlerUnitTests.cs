using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlGet;
using CustomCADs.Catalog.Domain.Products.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetCadUrlGet;

using static ProductsData;

public class GetProductCadPresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct();
    private const string url = "presigned-url";
    private const string contentType = "application/json";

    public GetProductCadPresignedUrlGetHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
            .ReturnsAsync(product);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlGetByIdQuery>(), ct))
            .ReturnsAsync((url, contentType));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        GetProductCadPresignedUrlGetQuery query = new(ValidId);
        GetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetProductCadPresignedUrlGetQuery query = new(ValidId);
        GetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlGetByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetProductCadPresignedUrlGetQuery query = new(ValidId);
        GetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

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

        GetProductCadPresignedUrlGetQuery query = new(ValidId);
        GetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
