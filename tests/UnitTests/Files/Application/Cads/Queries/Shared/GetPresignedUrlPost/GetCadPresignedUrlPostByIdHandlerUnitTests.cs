using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlPost;

public class GetCadPresignedUrlPostByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPostByIdHandler handler;
	private readonly Mock<ICadStorageService> storage = new();

	public const string Name = "CustomCAD";
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	public static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public GetCadPresignedUrlPostByIdHandlerUnitTests()
	{
		handler = new(storage.Object);

		storage.Setup(x => x.GetPresignedPostUrlAsync(Name, req))
			.ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetCadPresignedUrlPostByIdQuery query = new(Name, req);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPostUrlAsync(Name, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlPostByIdQuery query = new(Name, req);

		// Act
		UploadFileResponse result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
