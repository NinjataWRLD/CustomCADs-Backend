using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

using static ProductsData;

public class GetProductCadPresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
    private readonly Mock<IRequestSender> sender = new();
    private const string name = "product-name";
    private static readonly UploadFileRequest req = new("content-type", "file-name");
    private static readonly UploadFileResponse res = new("generated-key", "presigned-url");

    public GetProductCadPresignedUrlPostHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlPostByIdQuery>(), ct))
            .ReturnsAsync(res);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: req
        );
        CreatorGetProductCadPresignedUrlPostHandler handler = new(sender.Object);

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
        CreatorGetProductCadPresignedUrlPostQuery query = new(
            ProductName: name,
            Cad: req
        );
        CreatorGetProductCadPresignedUrlPostHandler handler = new(sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Equal(res, result);
    }
}
