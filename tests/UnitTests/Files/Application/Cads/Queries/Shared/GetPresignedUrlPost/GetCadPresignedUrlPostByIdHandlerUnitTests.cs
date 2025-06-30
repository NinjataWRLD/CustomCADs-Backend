using CustomCADs.Files.Application.Cads.Queries.Shared;
using CustomCADs.Shared.Abstractions.Storage;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.GetPresignedUrlPost;

public class GetCadPresignedUrlPostByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPostByIdHandler handler;
	private readonly Mock<IStorageService> storage = new();

	public const string Name = "CustomCAD";
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	public static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public GetCadPresignedUrlPostByIdHandlerUnitTests()
	{
		handler = new(storage.Object);

		storage.Setup(x => x.GetPresignedPostUrlAsync("cads", Name, req))
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
		storage.Verify(x => x.GetPresignedPostUrlAsync(
			"cads",
			Name,
			req
		), Times.Once);
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
