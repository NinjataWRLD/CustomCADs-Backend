using CustomCADs.Files.Application.Images.Queries.Shared;
using CustomCADs.Files.Application.Images.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Images.Queries;
using CustomCADs.UnitTests.Files.Application.Cads;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Shared.GetPresignedUrlPost;

public class GetImagePresignedUrlPostByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetImagePresignedUrlPostByIdHandler handler;
	private readonly Mock<IImageStorageService> storage = new();

	public const string Name = "CustomCAD";
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	public static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public GetImagePresignedUrlPostByIdHandlerUnitTests()
	{
		handler = new(storage.Object);

		storage.Setup(x => x.GetPresignedPostUrlAsync(Name, req))
			.ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetImagePresignedUrlPostByIdQuery query = new(Name, req);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPostUrlAsync(Name, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlPostByIdQuery query = new(Name, req);

		// Act
		UploadFileResponse result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
