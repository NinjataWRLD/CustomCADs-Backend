using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Post;

public class CreatorGetProductImagePresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductImagePresignedUrlPostHandler handler;
	private readonly Mock<IRequestSender> sender = new();

	private const string ProductName = "product-name";
	private static readonly UploadFileRequest req = new("content-type", "file-name");
	private static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public CreatorGetProductImagePresignedUrlPostHandlerUnitTests()
	{
		handler = new(sender.Object);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPostByIdQuery>(x => x.Name == ProductName && x.File == req),
			ct
		)).ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: ProductName,
			Image: req
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPostByIdQuery>(x => x.Name == ProductName && x.File == req),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreatorGetProductImagePresignedUrlPostQuery query = new(
			ProductName: ProductName,
			Image: req
		);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
