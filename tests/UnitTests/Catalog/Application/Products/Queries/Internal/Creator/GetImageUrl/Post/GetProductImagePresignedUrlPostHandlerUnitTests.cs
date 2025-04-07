using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

using static ProductsData;

public class GetProductImagePresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IRequestSender> sender = new();
    private const string productName = "product-name";
    private static readonly UploadFileRequest req = new("content-type", "file-name");
    private static readonly UploadFileResponse res = new("generated-key", "presigned-url");

    public GetProductImagePresignedUrlPostHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetImagePresignedUrlPostByIdQuery>(), ct))
            .ReturnsAsync(res);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreatorGetProductImagePresignedUrlPostQuery query = new(
            ProductName: productName,
            Image: req
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
            Image: req
        );
        CreatorGetProductImagePresignedUrlPostHandler handler = new(sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(res, result);
    }
}
