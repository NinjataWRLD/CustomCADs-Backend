using CustomCADs.Catalog.Application.Products.Queries.GetCadUrlPost;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.GetCadUrlPost;

using static ProductsData;

public class GetProductCadPresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IRequestSender> sender = new();
    private const string key = "generated-key";
    private const string url = "presigned-url";
    private const string productName = "product-name";
    private const string contentType = "content-type";
    private const string fileName = "file-name";

    public GetProductCadPresignedUrlPostHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlPostByIdQuery>(), ct))
            .ReturnsAsync((key, url));
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
            ProductName: productName,
            ContentType: contentType,
            FileName: fileName
        );
        GetProductCadPresignedUrlPostHandler handler = new(sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlPostByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        GetProductCadPresignedUrlPostQuery query = new(
            ProductName: productName,
            ContentType: contentType,
            FileName: fileName
        );
        GetProductCadPresignedUrlPostHandler handler = new(sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(key, result.GeneratedKey),
            () => Assert.Equal(url, result.PresignedUrl)
        );
    }
}
