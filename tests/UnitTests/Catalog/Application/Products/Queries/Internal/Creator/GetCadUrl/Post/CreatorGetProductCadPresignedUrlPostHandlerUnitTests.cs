using CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Post;

public class CreatorGetProductCadPresignedUrlPostHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreatorGetProductCadPresignedUrlPostHandler handler;
	private readonly Mock<IRequestSender> sender = new();

	private const string Name = "product-name";
	private static readonly UploadFileRequest req = new("content-type", "file-name");
	private static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public CreatorGetProductCadPresignedUrlPostHandlerUnitTests()
	{
		handler = new(sender.Object);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPostByIdQuery>(x => x.Name == Name && x.File == req),
			ct
		)).ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPostQuery query = new(
			ProductName: Name,
			Cad: req
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlPostByIdQuery>(x => x.Name == Name && x.File == req),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		CreatorGetProductCadPresignedUrlPostQuery query = new(
			ProductName: Name,
			Cad: req
		);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
