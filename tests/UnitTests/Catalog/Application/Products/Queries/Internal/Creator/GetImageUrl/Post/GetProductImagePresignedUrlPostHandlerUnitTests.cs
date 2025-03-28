using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using static ProductsData;

public class GetProductImagePresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IRequestSender> sender = new();
    private const string key = "generated-key";
    private const string url = "presigned-url";
    private const string productName = "product-name";
    private const string contentType = "content-type";
    private const string fileName = "file-name";

    public GetProductImagePresignedUrlPostHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetImagePresignedUrlPostByIdQuery>(), ct))
            .ReturnsAsync((key, url));
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPostQuery query = new(
            ProductName: productName,
            ContentType: contentType,
            FileName: fileName
        );
        CreatorGetProductImagePresignedUrlPostHandler handler = new(sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetImagePresignedUrlPostByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPostQuery query = new(
            ProductName: productName,
            ContentType: contentType,
            FileName: fileName
        );
        CreatorGetProductImagePresignedUrlPostHandler handler = new(sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(key, result.GeneratedKey),
            () => Assert.Equal(url, result.PresignedUrl)
        );
    }
}
