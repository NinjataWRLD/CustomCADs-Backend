using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Get;

using static ProductsData;

public class GetProductCadPresignedUrlGetHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IProductReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Product product = CreateProduct(creatorId: ValidDesignerId);
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
        CreatorGetProductCadPresignedUrlGetQuery query = new(ValidId, ValidDesignerId);
        CreatorGetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreatorGetProductCadPresignedUrlGetQuery query = new(ValidId, ValidDesignerId);
        CreatorGetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

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
        CreatorGetProductCadPresignedUrlGetQuery query = new(ValidId, ValidDesignerId);
        CreatorGetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(url, result.PresignedUrl),
            () => Assert.Equal(contentType, result.ContentType)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorized()
    {
        // Arrange
        CreatorGetProductCadPresignedUrlGetQuery query = new(ValidId, ValidCreatorId);
        CreatorGetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<Product>>(async () =>
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

        CreatorGetProductCadPresignedUrlGetQuery query = new(ValidId, ValidDesignerId);
        CreatorGetProductCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Product>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
