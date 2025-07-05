using CustomCADs.Customizations.Application.Materials.Queries.Internal.GetTextureUrl.Post;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Customizations.Application.Materials.Queries.GetTextureUrl.Post;

public class GetMaterialTexturePresignedUrlPostHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly GetMaterialTexturePresignedUrlPostHandler handler;
	private readonly Mock<IRequestSender> sender = new();

	private const string MaterialName = "material-name";
	private static readonly UploadFileResponse uploadRes = new("generated-key", "presigned-url");
	private static readonly UploadFileRequest uploadReq = new("content-type", "file-name");

	public GetMaterialTexturePresignedUrlPostHandlerUnitTests()
	{
		handler = new(sender.Object);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPostByIdQuery>(x => x.Name == MaterialName && x.File == uploadReq),
			ct
		)).ReturnsAsync(uploadRes);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPostQuery query = new(
			MaterialName,
			uploadReq
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetImagePresignedUrlPostByIdQuery>(x => x.Name == MaterialName && x.File == uploadReq),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetMaterialTexturePresignedUrlPostQuery query = new(
			MaterialName,
			uploadReq
		);

		// Act
		UploadFileResponse response = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(uploadRes, response);
	}
}
